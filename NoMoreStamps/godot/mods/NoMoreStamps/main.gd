extends Node

const MOD_ID = "NoMoreStamps"

onready var TackleBox := get_node_or_null("/root/TackleBox")

var config: Dictionary
var default_config: Dictionary = {
	"LargePastesFilter": false,
	"CustomCanvasFilter": false,
	"GIFFilter": false,
	"HideUsersChalkForLarge": false,
	"HideUsersChalkForCustom": false,
	"HideUsersChalkForGIF": true,
	"HostKickForLarge": false,
	"HostKickForCustom": false,
	"HostKickForGIF": false,
	"JoinDelayTime": 7
}

func _ready():
	config = default_config
	if TackleBox != null:
		TackleBox.connect("mod_config_updated", self, "_on_config_update")
		_init_config()
	pass

func _init_config() -> void:
	var saved_config = TackleBox.get_mod_config(MOD_ID)

	for key in default_config.keys():
		if not saved_config[key]: # If the config property isn't saved...
			saved_config[key] = default_config[key] # Set it to the default
	
	config = saved_config
	TackleBox.set_mod_config(MOD_ID, config) # Save it to a config file!

func _on_config_update(mod_id: String, new_config: Dictionary) -> void:
	if mod_id != MOD_ID:
		return

	if config.hash() == new_config.hash():
		return

	self.config = new_config
