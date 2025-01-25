extends Control

var plactor = null
var PlayerAPI
var loadedin = false
var tierval = 1


onready var timeslider = $Panel/maincontainer/A1/A2/timeslider
onready var tierslider = $Panel/maincontainer/B1/B2/tierslider.value

func _ready():
	PlayerAPI = get_node_or_null("/root/BlueberryWolfiAPIs/PlayerAPI")
	PlayerAPI.connect("_player_added", self, "init_player")
	$Panel.visible = false

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
	if plactor.held_item["id"] == "chalk_red" and Input.is_action_just_pressed("move_down") and $Panel.visible == false:
		$Panel.visible = true
	elif plactor.held_item["id"] == "chalk_red" and Input.is_action_pressed("move_down"):
		self.rect_position.y = lerp(self.rect_position.y, 0, delta*10)
	elif self.rect_position.y > -1080:
		self.rect_position.y = lerp(self.rect_position.y, -1100, delta*10)
	elif $Panel.visible == true:
		$Panel.visible = false


func _on_timeslider_value_changed(value):
	$Panel/maincontainer/A1/A2/duration.text = "duration: " + str(value) + " minutes"


func _on_tierslider_value_changed(value):
	var bread = str(value)
	match bread:
		"1":
			bread = "i"
			tierval = 1
		"2":
			bread = "ii"
			tierval = 2
		"3":
			bread = "iii"
			tierval = 3
		"4":
			bread = "iv"
			tierval = 4
		"5":
			bread = "v"
			tierval = 5
	$Panel/maincontainer/B1/B2/tier.text = "tier " + bread

func _bufflib_button(buff, boon):
	if buff != "" and boon == "":
		if not Input.is_action_pressed("move_walk"):
			plactor._add_buff(buff, timeslider.value*60, tierval)
		else:
			plactor._wipe_buff(buff)
	elif buff == "" and boon != "":
		if not Input.is_action_pressed("move_walk"):
			plactor._add_boon(boon, timeslider.value*60, tierval)
		else:
			plactor._wipe_boon(boon)
	elif buff == "" and boon == "":
		plactor._wipe_all_buffs()
		plactor._wipe_all_boons()
		


func _on_shrink_pressed():
	plactor.player_scale = clamp(plactor.player_scale - 0.1, 0.7, 1.3)


func _on_grow_pressed():
	plactor.player_scale = clamp(plactor.player_scale + 0.1, 0.7, 1.3)


func _on_catchboost_pressed():
	if not Input.is_action_pressed("move_walk"):
		plactor.catch_drink_timer += timeslider.value*60*60
		plactor.catch_drink_boost = 1.3
		plactor.catch_drink_reel = 1.45
		plactor.catch_drink_xp = 1.25
		plactor.catch_drink_tier = 3
		plactor.catch_drink_gold_add = Vector2(0, 0)
		plactor.catch_drink_gold_percent = 0.0
	else:
		plactor.catch_drink_timer = 5


func _on_booze_pressed():
	if not Input.is_action_pressed("move_walk"):
		plactor.drunk_timer += timeslider.value*60*60
		plactor.drunk_timer = clamp(plactor.drunk_timer, 0, 100000)
	else:
		plactor.drunk_timer = 5


func _on_movespeed_pressed():
	if not Input.is_action_pressed("move_walk"):
		plactor.boost_timer += timeslider.value*60*60
		plactor.boost_amt = 4.0
	else:
		plactor.boost_timer = 5


func _on_jumpboost_pressed():
	if not Input.is_action_pressed("move_walk"):
		plactor.jump_bonus_tier = 2
		plactor.jump_bonus_timer += timeslider.value*60*60
		plactor.jump_bonus = 4.0
		plactor.dive_bonus = 3.5
	else:
		plactor.jump_bonus_timer = 5
