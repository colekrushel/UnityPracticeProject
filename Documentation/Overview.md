Brief overview of how the game objects communicate with each other

important gameObjects
Main Camera
	- followplayer.cs
playerCharacter
	- characterMovement.cs
	- playerInteractController.cs
inspectTextbox
	- initializeCanvas.cs 



Scripts
Camera
	- followplayer.cs -> updates the position of the camera relative to the player
character 
	- characterMovement.cs -> handles player velocity and checks for jump button on update
	- ineractionCollider.cs -> handles detection of collision with interactable objects and displaying indicator and textbox on keypress "e" (DOESNT WORK :(
	- playerInteractionCollider -> interaction collider but its attached to the player gameobject directly instead of a child, this one does actually work!!
Interactables
	- inspectObject.cs -> UNUSED
	- interactController.cs -> UNUSED
UI
	- initializeCanvas.cs -> UNUSED
Helpers
	- EventManager.cs -> UNUSED