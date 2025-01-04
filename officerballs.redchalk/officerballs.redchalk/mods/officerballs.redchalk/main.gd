extends Node


var timecheck = 0
var loadedin = false
var plactor

var choice = -8

func _ready(): pass

func _process(delta):
	
	if Network.PLAYING_OFFLINE or Network.STEAM_LOBBY_ID <= 0:
		loadedin = false
		plactor = null
		return
		
	if timecheck > 0:
		timecheck -= 1
	elif timecheck <= 0:
		timecheck = 60
		for actor in get_tree().get_nodes_in_group("controlled_player"):
			if not is_instance_valid(actor): return
			else:
				if not loadedin:
					plactor = actor
					loadedin = true
	if loadedin:
		_get_input()

func _get_input():
	
	if not Input.is_action_pressed("move_walk") and not Input.is_action_pressed("move_sneak"): plactor.nyan_zoomlock = false
	
	if plactor.held_item["id"] == "chalk_red":
		if Input.is_action_just_pressed("move_walk"): plactor.nyan_zoomlock = true
		if Input.is_action_pressed("move_walk") and Input.is_action_just_released("zoom_in"):
			_chalk_menu(-3,true)
		elif Input.is_action_pressed("move_walk") and Input.is_action_just_released("zoom_out"):
			_chalk_menu(-5,true)
		
		if Input.is_action_pressed("move_walk") and Input.is_action_pressed("interact") and choice == 0:
			_chalk_menu(choice)
		elif Input.is_action_pressed("move_walk") and Input.is_action_pressed("move_down") and choice == 0:
			_chalk_menu(choice,false,true)
		
		if Input.is_action_just_pressed("interact") and Input.is_action_pressed("move_walk") and choice == 1:
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
		
func _chalk_menu(option,scroll=false,alt=false):
	if scroll:
		if option == -3:
			choice += 1
		elif option == -5:
			choice -= 1
		if choice < 0: choice = 3
		elif choice > 3: choice = 0
		match choice:
			0: PlayerData._send_notification("size menu")
			1: PlayerData._send_notification("movement buff menu")
			2: PlayerData._send_notification("catching buff menu")
			3: PlayerData._send_notification("movement reset")
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
