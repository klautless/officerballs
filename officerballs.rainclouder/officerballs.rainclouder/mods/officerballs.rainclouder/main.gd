extends Node

var timecheck = 0
var loadedin = false

var cloudIDs = []

var rainToggle = false
var rainPos = Vector3()
var rainZone = ""
var rainZoneOwner = -1
var raintimer = 0
var raintarget = 0


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
		rainToggle = false
		return
	elif not PlayerAPI.in_game: return
	
	elif PlayerAPI.in_game and plactor != null:
		raintimer = Time.get_unix_time_from_system()
		_get_input()
		if raintimer > raintarget:
			raintarget = Time.get_unix_time_from_system() + 45
			if cloudIDs.size() > 0:
				for cloud in cloudIDs:
					if cloud.yes == "rainmain":
						plactor._wipe_actor(cloud.id)
						cloudIDs.erase(cloud)
						continue
			var newCloud = Network._sync_create_actor("raincloud_tiny", rainPos, rainZone, - 1, Network.STEAM_ID, Vector3.ZERO, rainZoneOwner)
			cloudIDs.append({"id": newCloud, "yes": "rainmain"})

func _get_input():
	if not PlayerAPI.in_game or plactor == null: return
	if Input.is_action_just_pressed("move_jump") and Input.is_action_pressed("move_sneak"): 
		rainToggle = not rainToggle
		rainPos = plactor.global_transform.origin + Vector3(0,4.5,0)
		if rainToggle:
			PlayerData._send_notification("rain position updated")
			rainZone = plactor.current_zone
			rainZoneOwner = plactor.current_zone_owner if plactor.current_zone == "island_tiny_zone" or plactor.current_zone == "island_med_zone" or plactor.current_zone == "island_big_zone" else -1
			raintarget = 0
		if not rainToggle:
			PlayerData._send_notification("auto-rainer disabled")
			if cloudIDs.size() > 0:
					for cloud in cloudIDs:
						if cloud.yes == "rainmain":
							plactor._wipe_actor(cloud.id)
							cloudIDs.erase(cloud)
							continue
