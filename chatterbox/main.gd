extends Node

var loadedin = false
var plactor = null
var opacity = 0.25
var ccolor = Color(0.3, 0.3, 0.3, opacity)
var colorpicker
var state = ""
var PlayerAPI
var _keybinds_api

func _ready():

	_keybinds_api = get_node_or_null("/root/BlueberryWolfiAPIs/KeybindsAPI")
	
	var opacity_down_signal = _keybinds_api.register_keybind({
		"action_name": "opacity_down",
		"title": "Chat Opacity Down",
		"key": KEY_F7,
	})
	
	var opacity_up_signal = _keybinds_api.register_keybind({
		"action_name": "opacity_up",
		"title": "Chat Opacity Up",
		"key": KEY_F8,
	})
	
	var chatbox_color_signal = _keybinds_api.register_keybind({
		"action_name": "color_change",
		"title": "Show Color Picker",
		"key": KEY_F6,
	})
	
	_keybinds_api.connect(opacity_down_signal, self, "_opacity_down_down")
	_keybinds_api.connect(opacity_down_signal + "_up", self, "_opacity_down_up")
	_keybinds_api.connect(opacity_up_signal, self, "_opacity_up_down")
	_keybinds_api.connect(opacity_up_signal + "_up", self, "_opacity_up_up")
	
	_keybinds_api.connect(chatbox_color_signal, self, "_chatbox_color_down")
	_keybinds_api.connect(chatbox_color_signal + "_up", self, "_chatbox_color_up")

	PlayerAPI = get_node_or_null("/root/BlueberryWolfiAPIs/PlayerAPI")
	PlayerAPI.connect("_player_added", self, "init_player")
	
func init_player(player: Actor):
	if not loadedin: for i in 5:
		if loadedin: break
		yield (get_tree().create_timer(1),"timeout")
		if get_tree().get_nodes_in_group("controlled_player").size() > 0:
			for actor in get_tree().get_nodes_in_group("controlled_player"):
				if not is_instance_valid(actor): return
				else:
					if not loadedin:
						plactor = actor
						loadedin = true
						hudCorrect()

func _process(delta):
	if not PlayerAPI.in_game and loadedin:
		loadedin = false
		plactor = null
		return
	elif not PlayerAPI.in_game: return
	match state:
		"downing":
			opacity=clamp(opacity-delta,0,1)
			ccolor = Color(ccolor.r,ccolor.g,ccolor.b,opacity)
			var chat = plactor.hud.find_node("gamechat")
			var panel2 = chat.find_node("Panel2")
			panel2.self_modulate = ccolor
			
		"upping":
			opacity=clamp(opacity+delta,0,1)
			ccolor = Color(ccolor.r,ccolor.g,ccolor.b,opacity)
			var chat = plactor.hud.find_node("gamechat")
			var panel2 = chat.find_node("Panel2")
			panel2.self_modulate = ccolor

func round_to_dec(num, digit):
	return round(num * pow(10.0, digit)) / pow(10.0, digit)

func _chatbox_color_down() -> void:
	colorpicker.visible = not colorpicker.visible
	print("picker toggled")
	

func _chatbox_color_up() -> void:
	plactor.hud.find_node("ColorPicker").queue_free()
	print("picker toggle released")

func _adjust_color(color: Color) -> void:
	print("adjusting color")
	ccolor=color
	var chat = plactor.hud.find_node("gamechat")
	var panel2 = chat.find_node("Panel2")
	panel2.self_modulate = ccolor

func _opacity_down_down() -> void:
	print("opacity down -down")
	state = "downing"

func _opacity_down_up() -> void:
	print("opacity down -up")
	state = ""
	PlayerData._send_notification("chat opacity set to " + str(round_to_dec(opacity,2)*100) + "%")

func _opacity_up_down() -> void:
	print("opacity up -down")
	state = "upping"

func _opacity_up_up() -> void:
	print("opacity up -up")
	state = ""
	PlayerData._send_notification("chat opacity set to " + str(round_to_dec(opacity,2)*100) + "%")
	
func hudCorrect():
	var chat = plactor.hud.find_node("gamechat")
	var panel1 = chat.find_node("Panel")
	var panel2 = chat.find_node("Panel2")
	var text = chat.find_node("RichTextLabel")
	chat.rect_position.y = 592
	panel1.visible = false
	panel2.self_modulate = ccolor
	panel2.margin_right = 1290
	panel2.rect_size.x = 1783
	text.margin_right = 1284
	text.rect_size.x = 1765
	
	colorpicker = ColorPicker.new()
	colorpicker.presets_visible = false
	plactor.hud.add_child(colorpicker)
	colorpicker.rect_position.x = 1512
	colorpicker.rect_position.y = 259
	colorpicker.connect("color_changed", self, "_adjust_color")
	colorpicker.visible = false
	
	return
