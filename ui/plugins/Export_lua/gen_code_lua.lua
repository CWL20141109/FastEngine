local AS_TYPE = {
    ["GImage"] = "asImage",
    ["GComponent"] = "asCom",
    ["GButton"] = "asButton",
    ["GLabel"] = "asLabel",
    ["GProgressBar"] = "asProgress",
    ["GSlider"] = "asSlider",
    ["GComboBox"] = "asComboBox",
    ["GTextField"] = "asTextField",
    ["GRichTextField"] = "asRichTextField",
    ["GTextInput"] = "asTextInput",
    ["GLoader"] = "asLoader",
    ["GList"] = "asList",
    ["GGraph"] = "asGraph",
    ["GGroup"] = "asGroup",
    ["GMovieClip"] = "asMovieClip",
    ["GTree"] = "asTree",
    ["GTreeNode"] = "treeNode",
    ["GRoot"] = "root",
    ["GLoader3D"] = "asLoader3D"
}

local EXTENTION_TYPE = {
    ["Button"] = "asButton",
    ["ProgressBar"] = "asProgress",
    ["ComboBox"] = "asComboBox",
    ["Slider"] = "asSlider",
    ["Label"] = "asLabel"
}

---@class gen_code_lua
local msg = fclass()
---
local gen_lua_root_path = ""
local gen_lua_window_path = ""

local gen_class_lua_path = "" --根路径/包/
---UI类前缀
local gen_lua_prefix = "fairy"
---define类前缀
local gen_define_path = "fairy"
--- 是否导出window类
local gen_lua_window = true
local project_basePath = ""
---@type CS.System.IO
local cIO = CS.System.IO

---@param handler CS.FairyEditor.PublishHandler
function msg:ctor(handler)
    self.handler = handler
    ---@type CS.FairyEditor.FPackage
    self.package = handler.pkg
    ---@type CS.FairyEditor.GlobalPublishSettings.CodeGenerationConfig
    self.settings = handler.project:GetSettings("Publish").codeGeneration

    self.packageName = handler:ToFilename(handler.pkg.name) --convert chinese to pinyin, remove special chars etc.
    self.exportpath = handler.exportPath .. "\\" .. self.packageName
    self.exportCodePath = handler.exportCodePath .. "\\" .. self.packageName

    handler.genCode = false
    handler.exportPath = self.exportpath
    handler.exportCodePath = self.exportCodePath
    
    log(handler.exportCodePath)
    ---@type CS.FairyEditor.PublishHandler.ClassInfo[]
    self.classes = handler:CollectClasses(self.settings.ignoreNoname, self.settings.ignoreNoname, nil)

    handler:SetupCodeFolder(self.exportCodePath, "") --check if target folder exists, and delete old files
    handler:SetupCodeFolder(self.exportpath,"") -- 检查创建ui文件夹

    self:init()
end

---初始化
function msg:init()

    gen_lua_root_path = self.exportpath  .. get_custom_properties("gen_lua_root_path")
    gen_lua_window_path = self.exportpath  .. get_custom_properties("gen_lua_window_path")
    gen_lua_prefix = "fairy"
    --- define类前缀
    gen_define_path = "fairy"
    project_basePath = self.handler.project.basePath
    gen_class_lua_path = self:combine_path(gen_lua_root_path, self.handler.pkg.name)

    -- 清理目录
    -- self:directory_clear(gen_class_lua_path)
    self:execute()
end

function msg:execute()
    for i = 1, self.classes.Count - 1 do
        local class = self.classes[i]
        self:gen_component_code(class)
        self:gen_ui_code(class)
    end
end

--- 组件代码
---@param info CS.FairyEditor.PublishHandler.ClassInfo
function msg:gen_component_code(info)
    --- component type
    local comType = info.superClassName
    --- component name
    local comName = info.className
    --- lua file path
    local class_path = self:combine_path(gen_class_lua_path, self:get_class_name(comName))
    --- component url
    local class_url = "ui://" .. self.package.name .. "/" .. comName
    local template_path = self.handler.project.basePath .. "/template/component_lua.template"
    local template = cIO.File.ReadAllText(template_path)

    template = string.gsub(template, "{export_com_type}", comType)
    template = string.gsub(template, "{export_url}", class_url)

    local child_list = {}
    ---@type CS.FairyEditor.PublishHandler.MemberInfo[]
    local members = info.members
    local members_cnt = members.Count
    for j = 0, members_cnt - 1 do
        ---@type CS.FairyEditor.PublishHandler.MemberInfo
        local member_info = members[j]

        if self:check_member_name(member_info.name) or member_info.group ~= 1 then
            local field
            if member_info.group ~= 1 then
                field = "self.ui." .. member_info.name
            else
                field = "self.ui." .. member_info.name
            end
            local code = self:get_component_child_code(member_info)
            table.insert(child_list, "\t" .. field .. " = " .. code)
        end
    end

    -- 空内容的组件不导出
    if #child_list < 1 then
        return
    end

    local export_child = ""
    for i = 1, #child_list do
        export_child = export_child .. child_list[i] .. "\r\n"
    end
    template = string.gsub(template, "{export_child}", export_child)

    local file = io.open(class_path, 'w+b')
    log("class_path ----> "..class_path)
    io.output(file)
    io.write(template)
    io.close(file)
end

--- 界面代码
---@param info CS.FairyEditor.PublishHandler.ClassInfo
function msg:gen_ui_code(info)
    local com_name = info.className
    if self:is_window(com_name) then
        local fp = self:combine_path(gen_lua_window_path, com_name .. ".lua")
        if not self:exists_path(fp) then
            local wt_path = self:combine_path(self.handler.project.basePath)
            if not self:exists_path(wt_path) then
                msg:log_error("window模板丢失")
            else
                local template = cIO.File.ReadAllText(wt_path)
                template = string.gsub(template, "{component_name}", com_name)
                template = string.gsub(template, "{package_name}", self.package.name)

                local child_list = {}
                local event_list = {}
                ---@type CS.FairyEditor.PublishHandler.MemberInfo[]
                local members = info.members
                local member_cnt = members.Count
                for j = 0, member_cnt - 1 do
                    local member_info = members[j]
                    if self:check_member_name(member_info.name) then
                        if self:parse_member_type(member_info) == "asButton" then
                            -- 事件绑定
                            table.insert(
                                event_list,
                                string.format(
                                    "\tui.%s.onClick:Set(self.%_onclick,self)",
                                    member_info.name,
                                    member_info.name
                                )
                            )

                            table.insert(
                                child_list,
                                string.format("function window:" .. member_info.name .. "_onclick(context)\nend")
                            )
                        end
                    end
                end

                local export_child = ""
                for i = 1, #child_list do
                    export_child = export_child .. child_list[i] .. "\r\n"
                end

                local export_event = "\tself.handle = self._ins.windowHandle\n\tlocal ui = self.handle.ui\n"
                for i = 1, #event_list do
                    export_event = export_event .. event_list[i] .. "\n"
                end

                template = string.gsub(template, "{callback_function}", export_child)
                template = string.gsub(template, "{bind_event}", export_event)

                local file = io.open(fp, "w+b")
                io.output(file)
                io.write(template)
                io.close(file)
            end
        end
    end
end

-- define 导出
function msg:gen_require_define()
    local lcs = {}
    local dirs_root_list = {}

    table.insert(lcs, "--[[ aotu generated from fastfairy plugin]]\r")
    table.insert(lcs, "fairy ={}\r")

    --查找子目录列表
    local dirs_root = cIO.Directory.GetDirectories(gen_lua_root_path)
    dirs_root = dirs_root:GetEnumerator()
    while dirs_root:MoveNext() do
        local v = dirs_root.Current
        table.insert(dirs_root_list, v)
    end

    for i = 1, #dirs_root_list do
        local pack_name = string.gsub(dirs_root_list[i], gen_lua_root_path, "")
        local file_names = "\t"
        table.insert(lcs, "\n\n--" .. pack_name .. "\n")

        -- 获取文件夹下面的文件
        local dirs = cIO.Directory.GetFiles(dirs_root_list[i])
        dirs = dirs:GetEnumerator()
        while dirs:MoveNext() do
            local v = dirs.Current
            local temp = self:split(v, ".")
            if temp[#temp] ~= "meta" then
                local fn = self:parse_dirs_name(v, pack_name)
                file_names = file_names .. fn .. ",\n\t\t"
            end
        end

        local code =
            'function fairy.require_%s(loadfile)\r\n\tlocal pname = "%s"\r\n\tlocal pcnames = {\n\t%s\n\t}\r\n\tfui.require_fairy_lua(pname, pcnames, loadfile)\r\nend'
        code = string.format(code, pack_name, pack_name, file_names)
        table.insert(lcs, code)
    end

    local str = ""
    for i = 1, #lcs do
        str = str .. lcs[i]
    end

    local file = io.open(self:combine_path(gen_lua_root_path, "/define.lua", "w+b"))
    io.output(file)
    io.write(str)
    io.close(file)
end
function msg:log_error(msg)
    CS.FairyEditor.App.consoleView:LogError(tostring(msg))
end

--- 检查是否忽略导出
---@param name string 名字
function msg:check_member_name(name)
    return string.sub(name, 1, 3) == "ig_"
end

--- 分割字符串
---@return string[]
---@param str string
---@param delimiter string
function msg:split(str, delimiter)
    if (delimiter == "") then
        return false
    end
    local pos, arr = 0, {}
    -- for each divider found
    for st, sp in function()
        return string.find(str, delimiter, pos, true)
    end do
        table.insert(arr, string.sub(str, pos, st - 1))
        pos = sp + 1
    end
    table.insert(arr, string.sub(str, pos))
    return arr
end

---拼接路径
---@return string
---@param path1 string
---@param path2 string
function msg:combine_path(path1, path2)
    return path1 .. path2
end

--- 删除目录
---@param path string
function msg:directory_clear(path)
    local tempTable = {}
    --统计
    log("path --->"..path)
    local dirs_root = cIO.Directory.GetFiles(path)
    dirs_root = dirs_root:GetEnumerator()
    while dirs_root:MoveNext() do
        local v = dirs_root.Current
        table.insert(tempTable, v)
    end
    --删除
    for i = 1, #tempTable do
        if self:_existsPath(tempTable[i]) then
            cIO.File.Delete(tempTable[i])
        end
    end
end

--- UI类名拼接
function msg:get_class_name(cname)
    if gen_lua_prefix == "" then
        return "/" .. cname .. ".lua"
    end
    return "/" .. gen_lua_prefix .. "_" .. cname .. ".lua"
end

--- 组件名称解析
---@param member CS.FairyEditor.PublishHandler.MemberInfo
function msg:get_component_child_code(member)
    -- 组件
    if member.group == 0 then
        return "self:GetChildAt(" .. member.index .. ")." .. self:parse_member_type(member) .. ";"
    elseif member.group == 1 then
        return "self:GetControllerAt(" .. member.index .. ");"
    else
        return "self:GetTransitionAt(" .. member.index .. ");"
    end
end
--- 类型转换
---@param member CS.FairyEditor.PublishHandler.MemberInfo
function msg:parse_member_type(member)
    local ts = self:split(member.type, ".")
    local key = ts[#ts]
    local type = AS_TYPE[key]
    if not type then
        if member.res ~= nil then
            local res = member.res
            local filePath = res.owner.basePath .. res.path .. "/" .. res.name .. ".xml"
            local xml = CS.FairyEditor.XMLExtension.Load(filePath)
            local extention = xml:GetAttribute("extention")
            if extention ~= nil then
                return EXTENTION_TYPE[extention] or "extention type:" .. extention
            end
            return "asCom"
        end
        return ""
    end
    return type
end

--- 是不是界面window
---@param name string 名字
function msg:is_window(name)
    local result = self:split(name, "_")
    local len = #result
    if len > 1 then
        if result[len] == "window" then
            return true
        end
    end
    return false
end

---查找路径下有没有对应文件
---@param path string 路径
function msg:exists_path(path)
    return cIO.File.Exists(path)
end

-- 解析文件名字
function msg:parse_dirs_name(dirs_name, pack_name)
    dirs_name = string.gsub(dirs_name, gen_lua_root_path, "")
    dirs_name = string.gsub(dirs_name, ".lua", "")
    dirs_name = string.gsub(dirs_name, "\\", "/")
    dirs_name = string.sub(dirs_name, string.len(pack_name) + 2, -1)
    return '"' .. dirs_name .. '"'
end

return msg
