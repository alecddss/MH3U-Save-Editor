# Monster Hunter 3 Ultimate (Wii U) Save Editor

A simple and intuitive save editor that allows you to inject items, weapons, armor, and talismans right into your boxes, and zenny and points right into your wallet. Gone are the days of opening your save file in a hex editor, and searching for your item's hex value from D7S's (admittedly well-organized) post on gbatemp. 

This is still a work-in-progress, but the following is completed:
  - Items can be saved to the item box.
  - Equipment can be saved to the equipment box (Armor, weapons and talismans). Each piece of equipment can be given decorations, armor can be upgraded and talismans can be given skills.
  - You can change all aspects of your Hunter. Don't like what you made in the character customizer at the beginning of the game? No problem, just find the options you'd like and select them!
  - Zenny, resource points, hunter rank, and time played are all modifiable.

In the future, I hope to add the following:
  - Change how far you are in the village quest story (I.e., from 1 star to 3 star).
  - Change how far you are in hub quests (I.e., skip over HR and go directly into GR).

## Disclaimers:

- This save editor is intended for use on a Monster Hunter 3 Ultimate (Wii U) save file using the Cemu emulator. If you are able to extract your save file from your Wii U, it may also work, but I have not tried it.
- Additionally, this application was created in Windows and may not work on other operating systems.
- Finally, this application was tested using a North American version of Monster Hunter 3 Ultimate (Wii U), and so other versions may not work as intended.

## Known Issues

- When runnning MH3U Save Editor.exe, you may get a Windows SmartScreen alert notifying you that this program has an "unknown publisher". This is normal. To avoid this alert, I have to pay a subscription to buy a certificate from a known publisher. I can't currently afford this, especially since this project is something I was doing for fun in my downtime. For what it's worth, I assure you nothing in this application can harm your computer.

## Pre-Requisite:

1. Locate your Monster Hunter 3 Ultimate (MH3U) save file on your computer. The easiest way is to open Cemu, then right-click on MH3U > Save directory > user > 80000001.
2. In this folder, you should have many files with names like "card1", "phrase1", etc. The file you need is dependent on which save file slot you chose in-game. If you chose the first slot, then your save file is named "user1". Unsurprisingly, it's "user2" for slot 2, and "user 3" for the last slot.
3. Once you located your save file, it's best to copy & paste it somewhere as a backup (just in case anything goes wrong when editing).

## How to Use:

1. Open MH3U Save Editor.exe.
2. Click File, then Open File (in the top left corner).
3. In the new pop-up window, navigate to your MH3U save file (which you just found in the pre-requisite section) and open it.
4. The application will update the right side with your save file's item box, in a 10 by 10 grid. Each button in the grid will list the item name and amount within it (i.e. Mega Potion x99).
5. If the words are cut-off, you can hover over the button with your mouse (don't click) and a hint pop-up will tell you the info of the item.
6. Above the grid is a drop-down menu listing Box Page 1-10. As the name suggests, change this value to change the page of the box.

### Item Box Tab
1. To add items into your save file, simply find the item in the Item drop-down menu (on the left side of the application).
2. Select the amount of that item you want (0-99).
3. Then click on the spot of the grid you wish (right side of application).
4. In game, the item will be in the exact place of the item box as it was in the editor.

### Equipment Box Tab
- When switching to the Equipment Box tab, the grid will update from displaying your item box data to your equipment box data.
- Instead of displaying the item name and amount, each square will display only the name of the equipment.
- However, when hovering over the square, you will see the equipment name, equipment type, number of upgrades, and the status of each decoration slot.
- If you decide to add more items, switching back to the Item Box Tab will update the grid again to display your item box.
- To add equipment:
	1. Select your intended equipment type (i.e. Chest Armor).
	2. Select the name of your equipment (i.e. Nether Mail).
	3. If you selected armor, pick how many upgrades you want it to have (only armor can be upgraded), from 0 to 255.
	4. Populate the slots with your intended decorations
	5. If you selected a talisman, pick talisman skills and how many points to put in that skill. Each skill can be given points from -128 to 127. Be aware that any point past 127 points will cause you to have negative points in that skill (i.e. 127 skill points + 1 skill point from a Lv 1 Jewel = total -128 skill points).
	6. Click on your desired spot in the equipment box grid to save your equipment.

>[!IMPORTANT]
>You must make sure the number of decorations fit within the slots of that armor. In the current save editor, there's no way to check how many decoration slots are in a piece of armor. I recommend checking Kiranico.com for that information. Also, keep in mind Lv 2 and 3 Jewels take up 2 and 3 slots respectively. If you slot more decorations than the armor has slots, it will crash the game once you view it in your box.

Additionally, you will find items like DUMMY or No Name within each equipment name list. These equipment are typically underpowered or useless, however, some of them have extremely inflated stats. For your convenience, I have made the last option of Great Sword, Sword and Shield, Hammer, and Bow, one of those weapons with inflated stats (I could not find any for the other weapon types, unfortunately).

### Player Character Tab
- This tab is almost entirely unfinished. As of now, only the following is implemented:
	- Changing your Hunter's name. You're normally allowed 10 characters, but with the save editor, you can use a max of 24 (your name might spill out of text boxes and have unforeseen issues if your name is too long, though)
	- Changing your clothing (not your armor, but what you wear without armor)
	- Changing your clothing color. The color of the button is your current color. Click on the button to open a color selector, and pick the color you would like. This selected color will overwrite the color you had before
	- Changing hair color. Same as changing clothing color
- Every other aspect of the Player Character Tab has not been found in the save file yet. Once I do find it, I'll update the editor accordingly.

### Misc. Edits Tab
This tab allows you change your zenny and resource points, as well as your time played. There is a disclaimer mentioning not to go over the max zenny/resource points limit of 2,147,483,647. I tried to be as concise as possible when explaining, but if you require a more thorough explanation, see the note below. HR and GR editing will be added whenever I'm able to find their location in the save file.

>[!NOTE]
>The reason why there is a limit of 2,147,483,647 zenny/points is due to the way the game stores these values. The game uses 4 hex bytes to save your currency which has a minimum of [00 00 00 00] (this is equal to 0) and a maximum of [FF FF FF FF] (this is equal to 4,294,967,296). Now you might be wondering why 4,294,967,296 is not the maximum. This is because the first half of this 4,294,967,296 (A.K.A. 2,147,483,647) are read and understood by the game as positive numbers. This leaves the second half as negative numbers. So if you were to count by 1 over and over, you'd go: 0, 1, 2, 3, ..., 2,147,483,646, 2,147,483,647, -2,147,483,648, -2,147,483,647, ..., -3, -2, -1 (-1 is the very last number).  
So what does this mean for you? This means that if you have the exact max of 2,147,483,647 currency, and gain even 1 zenny or point, you now have -2,147,483,648 of that currency. This is REALLY bad because the game treats it as a negative, and won't allow you to spend that currency until you're back positive again. Which means you need to gain 2,147,483,648 currency just to make it back to 0. Due to this little quirk in the programming, I strongly recommend you give yourself space between your desired currency and the max. Something like 2,000,000,000 would work well, since you would need to make nearly 150 million currency to roll into the negative (something which is unlikely to do unless you plan on really grinding the game).

### Saving file
Your file is completely unaffected until you hit save. If you decide you don't want to save your changes, simply close the application. Assuming you want to save your file, click File and then you can either click "Save File" to overwrite the save file you opened to start with, or "Save As..." to create another file to save your data to. Once you hit save, your file is saved and you can safely close the application.

>[!IMPORTANT]
>Your edited save file must be named "user1", "user2" or "user3" and placed back into the 80000001 folder (found in the pre-requisite section) in order for the game to read it. If you opened a user file from the 80000001 folder to begin with, and clicked "Save File" (instead of "Save As..."), then you don't need to worry about this.

## Special Thanks:

@D7S		(https://gbatemp.net/members/d7s.432023/)

@KeyZiro	(https://gbatemp.net/members/keyziro.403310/)

@KingHall6100	(https://gbatemp.net/members/kinghall6100.431506/)

@KokoPuffs	(https://gbatemp.net/members/kokopuffs.434477/)

It was thanks to these users on gbatemp that this application even had a chance to be made. Thanks to their work to complete a list of every item/ piece of equipment in the game, as well as their hex data, I was able to complete this save editor. I couldn't have done it without them!
