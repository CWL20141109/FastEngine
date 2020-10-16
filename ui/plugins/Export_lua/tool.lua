
function log(str)
    fprint(str)
end

local custom_properties = {}
---返回设置参数
---@param key string
---@return string value
function get_custom_properties(key)
    local value = custom_properties[key]
    if value == nil then
        ---@type CS.FairyEditor.CustomProps
        local settings = App.project:GetSettings("CustomProperties")
        local elements = settings.elements:GetEnumerator()
        while elements:MoveNext() do
            custom_properties[elements.Current.Key] = elements.Current.Value
        end

        value = custom_properties[key]
    end
    return value
end