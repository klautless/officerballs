extends Node

var timecheck = 0
var loadedin = false
var plactor

func _ready(): get_tree().connect("node_added", self, "_loadcheck")

func _loadcheck(node: Node):
	var scene: Node = get_tree().current_scene
	if scene.name == "world":
		for i in 5:
			if loadedin: break
			yield (get_tree().create_timer(1),"timeout")
			if get_tree().get_nodes_in_group("controlled_player").size() > 0:
				for actor in get_tree().get_nodes_in_group("controlled_player"):
					if not is_instance_valid(actor): return
					else:
						if not loadedin:
							plactor = actor
							loadedin = true
	else:
		loadedin = false
		plactor = null

func _process(delta):
	
	if get_tree().get_nodes_in_group("controlled_player").size() == 0:
		loadedin = false
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
		if randf() < 0.06:
			var a = (randf() - 0.5) * 1.2
			var b = randf() - 0.5
			var c = (randf() - 0.5) * 1.2
			plactor._sync_particle("music",Vector3(a,b,c))
