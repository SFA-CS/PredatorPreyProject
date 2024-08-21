// If you make a new scene to add a new version of the game, remember to change the game options script to include the new game version you created in the enum 'Version' and in the array of strings 'Version Name'
// Additionally, in the MainMenu scene, make sure to match up the version drop down (in the VersionObject), with the enum 'Version'

/* If you want to add more custom backrounds, prey, and predators:
 *      1. Add the sprite you want to use in the corresponding sprites list in the SpriteLoader script component of the CustomObjects
 *      2. Add an option that corresponds to the sprite you added to the corresponding dropdown menu 
 *      3. Add the option with the same name and spelling as what you called it in the dropdown menu to the corresponding enum in the GameOptions script
 *      4. Ensure the game runs properly when you do this, as you might have to add extra changes, such as changing the rotation and/or position of various transforms (similar to how we handle spaceships)
 */