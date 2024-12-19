class_name Menu
extends Control


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	start_button.button_up.connect(on_start_pressed)
	quit_button.button_up.connect(on_quit_pressed)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
	
@onready var start_button: Button = $VBoxContainer/StartButton as Button
@onready var quit_button: Button = $VBoxContainer/QuitButton as Button
@onready var start_level = preload("res://Scenes/Levels/Test.tscn") as PackedScene

func on_start_pressed() -> void:
	get_tree().change_scene_to_packed(start_level)
	
func on_quit_pressed() -> void:
	get_tree().quit();
