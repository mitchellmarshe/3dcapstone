1. Using FPSController prefab with attached Interact canvas, drag the FPSCOntroller into scene

2.Add UI>Event System to scene

3.Create NPC to talk to

4. Drag one of the DefaultButtonCanvas' onto the NPC

5. Add dialogInteractable Script to NPC

6. On dialogInteractable script, drag the FPSCOntroller to controller
	InteractCanvas to Interact UI
	The buttonCanvas to activate

7. Now go into the button canvas and add to
	
	DescriptionText = NArrative/NPC text to the player
	Responses>ResponseButton>ResponseText = Dialog option that the play can choose from

8. If using more then 1 button canvas for branching dialog:
	Go to each Response Button, OnCLick, and add

	
	drag The Next Button Canvas/ the one you want to now display: Gameobject.setActive check
	drag The current button canvas/ the one you just clicked on : GameObject.setActive uncheck
	drag the NPC that contains the dialogInteractable script : dialogInteractable.RegisterNewCanvas
								Then drag in the Next Canvas to display

	If a button needs to be the last response and then close the dialog, drag the current canvas over
			: GameObject.setActive uncheck


9.Repeat steps as needed
	