extends VBoxContainer


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	$StartButton.grab_focus()


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_StartButton_pressed():
	pass # Replace with function body.


func _on_OptionButon_pressed():
	pass # Replace with function body.


func _on_ExitButton_pressed():
	get_tree().quit(0)
