extends Node

var timecheck = 0
var loadedin = false
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
		if randf() < 0.06:
			var a = (randf() - 0.5) * 1.2
			var b = randf() - 0.5
			var c = (randf() - 0.5) * 1.2
			plactor._sync_particle("music",Vector3(a,b,c))
