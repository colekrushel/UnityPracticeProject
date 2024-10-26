
objectEnter: Triggers when player box collider collides with an object box collider. Toggles the display of the indicator above the player, and toggles listening for objectInspect events.

objectInspect: Triggers whenever a keypress "e" is detected, is only listened for when a player is within an object's box collider. Toggles the display of the textbox and updates it with text associated with the object (WIP)

objectLeave: Triggers when player box collider collides with an object box collider. Toggles the display of the indicator above the player, and toggles listening for objectInspect events.

(maybe combine objectEnter and objectLeave into one objectToggle event?)