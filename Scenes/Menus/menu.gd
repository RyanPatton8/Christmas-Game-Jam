class_name Menu
extends Control
	
@onready var start_button: TextureButton = $HBoxContainer/VBoxContainer/StartButton as TextureButton
@onready var quit_button: TextureButton = $HBoxContainer/VBoxContainer/QuitButton as TextureButton
@onready var start_level = preload("res://Scenes/Levels/Test.tscn") as PackedScene

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	start_button.button_up.connect(on_start_pressed)
	quit_button.button_up.connect(on_quit_pressed)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func on_start_pressed() -> void:
	get_tree().change_scene_to_packed(start_level)
	
func on_quit_pressed() -> void:
	get_tree().quit();
