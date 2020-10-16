---@type gen_code_lua
local msg = require(PluginPath .. "/gen_code_lua")
require(PluginPath.."/tool")

function onPublish(handler)
    -- if not handler.genCode then return end
    -- handler.genCode = false --prevent default output

    log("Handling gen code in plugin")

    local gen_code_lua = msg.new(handler)
end
function onDestroy()
    -------do cleanup here-------
end
