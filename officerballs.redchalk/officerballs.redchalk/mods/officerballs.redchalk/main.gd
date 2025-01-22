extends Node


var timecheck = 0
var loadedin = false

var choice = -8
var customchoices = -30

var plactor = null

var PlayerAPI

func _ready():
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
func _process(delta):
	if not PlayerAPI.in_game and loadedin:
		loadedin = false
		plactor = null
		return
	elif not PlayerAPI.in_game: return
	
	elif PlayerAPI.in_game and plactor != null:
		_get_input()

func _get_input():
	
	if not Input.is_action_pressed("move_walk") and not Input.is_action_pressed("move_sneak"): plactor.nyan_zoomlock = false
	
	if plactor.held_item["id"] == "chalk_red":
		if Input.is_action_just_pressed("move_walk"): plactor.nyan_zoomlock = true
		if Input.is_action_pressed("move_walk") and not Input.is_action_pressed("move_down")  and Input.is_action_just_released("zoom_in"):
			_chalk_menu(-3,true)
		elif Input.is_action_pressed("move_walk") and not Input.is_action_pressed("move_down")  and Input.is_action_just_released("zoom_out"):
			_chalk_menu(-5,true)
		
		if Input.is_action_pressed("move_walk") and Input.is_action_pressed("move_down") and Input.is_action_just_released("zoom_in"):
			_custom_buff_scroller()
		elif Input.is_action_pressed("move_walk") and Input.is_action_pressed("move_down") and Input.is_action_just_released("zoom_out"):
			_custom_buff_scroller(true)
		
		
		if Input.is_action_pressed("move_walk") and Input.is_action_pressed("interact") and choice == 0:
			_chalk_menu(choice)
		elif Input.is_action_pressed("move_walk") and Input.is_action_pressed("move_down") and choice == 0:
			_chalk_menu(choice,false,true)
		
		if Input.is_action_just_pressed("interact") and Input.is_action_pressed("move_walk") and (choice == 1 or choice == 4):
			_chalk_menu(choice)
		elif Input.is_action_just_pressed("move_down") and Input.is_action_pressed("move_walk") and choice == 1:
			_chalk_menu(choice,false,true)
			
		if Input.is_action_just_pressed("interact") and Input.is_action_pressed("move_walk"):
			if choice == 2 or choice == 3:
				_chalk_menu(choice)
		if Input.is_action_just_pressed("move_down") and Input.is_action_pressed("move_walk"):
			if choice == 2:
				_chalk_menu(choice,false,true)
		
		if Input.is_action_just_pressed("move_down") and Input.is_action_pressed("move_sprint") and Input.is_action_pressed("move_walk") and choice == 3:
			plactor.animation_data["alert"] = not plactor.animation_data["alert"]

func _custom_buff_scroller(back=false):
	if not back:
		customchoices += 1
		if customchoices >= 27: customchoices = 0
		elif customchoices <= 0: customchoices = 26
	else:
		customchoices -= 1
		if customchoices >= 27: customchoices = 0
		elif customchoices <= 0: customchoices = 26
	match customchoices:
		0: PlayerData._send_notification("buff_salty selected")
		1: PlayerData._send_notification("buff_fresh selected")
		2: PlayerData._send_notification("buff_rain selected")
		3: PlayerData._send_notification("buff_void selected")
		4: PlayerData._send_notification("buff_alien selected")
		5: PlayerData._send_notification("buff_small selected")
		6: PlayerData._send_notification("buff_large selected")
		7: PlayerData._send_notification("buff_quality selected")
		8: PlayerData._send_notification("buff_rarity selected")
		9: PlayerData._send_notification("buff_timestretch selected")
		10: PlayerData._send_notification("buff_cantlosefish selected")
		11: PlayerData._send_notification("buff_double selected")
		12: PlayerData._send_notification("buff_haste selected")
		13: PlayerData._send_notification("buff_clickreduce selected")
		14: PlayerData._send_notification("buff_clickmultiply selected")
		15: PlayerData._send_notification("buff_gatereduce selected")
		16: PlayerData._send_notification("buff_valuelift selected")
		17: PlayerData._send_notification("buff_efficiency selected")
		18: PlayerData._send_notification("buff_speedbuddies selected")
		19: PlayerData._send_notification("buff_doublebuddies selected")
		20: PlayerData._send_notification("buff_protection selected")
		21: PlayerData._send_notification("buff_gamblefisher selected")
		22: PlayerData._send_notification("boon_redcreep selected")
		23: PlayerData._send_notification("boon_weakening selected")
		24: PlayerData._send_notification("boon_slowbite selected")
		25: PlayerData._send_notification("boon_trash selected")
		26: PlayerData._send_notification("boon_slowness selected")

func _chalk_menu(option,scroll=false,alt=false):
	if scroll:
		if option == -3:
			choice += 1
		elif option == -5:
			choice -= 1
		if choice < 0: choice = 4
		elif choice > 4: choice = 0
		match choice:
			0: PlayerData._send_notification("size menu")
			1: PlayerData._send_notification("movement buff menu")
			2: PlayerData._send_notification("catching buff menu")
			3: PlayerData._send_notification("movement reset")
			4: PlayerData._send_notification("buff library buffs/boons")
	else:
		match choice:
			
			0:
				if not alt:
					var target = plactor.player_scale
					target += 0.01
					target = clamp(target,0.7,1.3)
					plactor.player_scale = lerp(plactor.player_scale, target, 0.2)
				else:
					var target = plactor.player_scale
					target -= 0.01
					target = clamp(target,0.7,1.3)
					plactor.player_scale = lerp(plactor.player_scale, target, 0.2)
					
			1: 
				if not alt:
					if not Input.is_action_pressed("move_sprint"):
						plactor.boost_timer += 3600
						plactor.boost_amt = 4.0
					else:
						plactor.jump_bonus_tier = 2
						plactor.jump_bonus_timer += 600
						plactor.jump_bonus = 4.0
					
			2: 
				if not alt:
					plactor.catch_drink_timer += 90000
					plactor.catch_drink_boost = 1.3
					plactor.catch_drink_reel = 1.45
					plactor.catch_drink_xp = 1.25
					plactor.catch_drink_tier = 3
					plactor.catch_drink_gold_add = Vector2(0, 0)
					plactor.catch_drink_gold_percent = 0.0
				else:
					plactor.catch_drink_timer = 0
				
			3:
				plactor.boost_timer = 0
				plactor.jump_bonus_timer = 0
			
			4:
				match customchoices:
					0: plactor.ob_buffs["buff_salty"] += 300
					1: plactor.ob_buffs["buff_fresh"] += 300
					2: plactor.ob_buffs["buff_rain"] += 300
					3: plactor.ob_buffs["buff_void"] += 300
					4: plactor.ob_buffs["buff_alien"] += 300
					5: plactor.ob_buffs["buff_small"] += 300
					6: plactor.ob_buffs["buff_large"] += 300
					7: plactor.ob_buffs["buff_quality"] += 300
					8: plactor.ob_buffs["buff_rarity"] += 300
					9: plactor.ob_buffs["buff_timestretch"] += 300
					10: plactor.ob_buffs["buff_cantlosefish"] += 300
					11: plactor.ob_buffs["buff_double"] += 300
					12: plactor.ob_buffs["buff_haste"] += 300
					13: plactor.ob_buffs["buff_clickreduce"] += 300
					14: plactor.ob_buffs["buff_clickmultiply"] += 300
					15: plactor.ob_buffs["buff_gatereduce"] += 300
					16: plactor.ob_buffs["buff_valuelift"] += 300
					17: plactor.ob_buffs["buff_efficiency"] += 300
					18: plactor.ob_buffs["buff_speedbuddies"] += 300
					19: plactor.ob_buffs["buff_doublebuddies"] += 300
					20: plactor.ob_buffs["buff_protection"] += 300
					21: plactor.ob_buffs["buff_gamblefisher"] += 300
					22: plactor.ob_boons["boon_redcreep"] += 300
					23: plactor.ob_boons["boon_weakening"] += 300
					24: plactor.ob_boons["boon_slowbite"] += 300
					25: plactor.ob_boons["boon_trash"] += 300
					26: plactor.ob_boons["boon_slowness"] += 300
