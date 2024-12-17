using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Collections;

namespace main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Will probably not end up implementing this, no real point in it. Invisible option for the time being
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //Functionality for Item Override checkbox on Item Box tab
            if (checkBox1.Checked)
            {
                panel2.Enabled = false;
                panel3.Enabled = true;
            }

            else
            {
                panel2.Enabled = true; 
                panel3.Enabled = false;
            }
        }

        private void buttonColorClothing_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.FullOpen = true;
            colorDlg.AllowFullOpen = true;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.Color = Color.Red;

            //Change color of button to match the color chosen
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                buttonColorClothing.BackColor = colorDlg.Color;
            }
        }

        private void buttonColorSkinTone_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.FullOpen = true;
            colorDlg.AllowFullOpen = true;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.Color = Color.Red;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                buttonColorSkinTone.BackColor = colorDlg.Color;
            }
        }

        private void buttonColorHairstyle_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.FullOpen = true;
            colorDlg.AllowFullOpen = true;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.Color = Color.Red;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                buttonColorHairstyle.BackColor = colorDlg.Color;
            }
        }

        private void buttonColorFeature2_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.FullOpen = true;
            colorDlg.AllowFullOpen = true;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.Color = Color.Red;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                buttonColorFeature2.BackColor = colorDlg.Color;
            }
        }

        private void buttonColorFeature1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            colorDlg.FullOpen = true;
            colorDlg.AllowFullOpen = true;
            colorDlg.AnyColor = true;
            colorDlg.SolidColorOnly = false;
            colorDlg.Color = Color.Red;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                buttonColorFeature1.BackColor = colorDlg.Color;
            }
        }

        //Initializing saveData outside of a method so that all methods can easily access it
        //Also initializing saveFile outside of a method, so that overwriting the save file (with File > Save File) is easier
        byte[] saveData;
        string saveFile = "";

        //When File > Open File is clicked, opens MH3U save file, and updates the save editor
        private void openFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "Select File";
            fd.Filter = "All files (*.*)|*.*";
            fd.ShowDialog();

            //Preliminary check to make sure the file has no extension (valid MH3U save file should not have one)
            if (fd.FileName != "" && fd.FileName.Contains(".") == false)
            {
                saveFile = fd.FileName;

                //Reads all bytes from hex file
                saveData = File.ReadAllBytes(saveFile);

                //Checks that the save file is a valid save file by making sure it is as long as a save file should be
                bool validSaveFile = false;
                try
                {
                    byte test = saveData[35364]; // This address should *not* exist, and should throw an error
                }

                catch
                {
                    try
                    {
                        byte test = saveData[35363]; // This address *should* exist, and should not throw an error
                        validSaveFile = true; // Since nothing was caught from the above statement, can set validSaveFile to true
                    }

                    catch
                    {
                        System.Windows.Forms.MessageBox.Show("The file you selected has insufficient data! Please ensure the file is a valid MH3U save " +
                            "file and has no file extension. \n(For example, the file may be named \"user1\")");
                    }
                }
                
                //Again, double checks that it's a valid save file
                if (validSaveFile == true)
                {
                    //Enabling all features that first require a save file to be read
                    saveAsToolStripMenuItem.Enabled = true;
                    saveFileToolStripMenuItem.Enabled = true;
                    tabControl1.Enabled = true;
                    comboBoxPage.Enabled = true;

                    //Initializing the Item and Item Amount boxes
                    comboBoxItems.SelectedIndex = 0;
                    numericUpDownAmount.Text = "0";

                    //Initializing all variables and populating them with their related data from saveData
                    byte[] playerName = new byte[] { saveData[43], saveData[44], saveData[45], saveData[46], saveData[47], saveData[48], saveData[49], 
                        saveData[50], saveData[51], saveData[52], saveData[53], saveData[54], saveData[55], saveData[56], saveData[57], saveData[58], 
                        saveData[59], saveData[60], saveData[61], saveData[62], saveData[63], saveData[64], saveData[65], saveData[66] };
                    byte clothing = saveData[206];
                    byte[] zenny = new byte[] { saveData[72], saveData[73], saveData[74], saveData[75] };
                    byte[] resource = new byte[] { saveData[23376], saveData[23377], saveData[23378], saveData[23379] };
                    byte[] time = new byte[] { saveData[76], saveData[77], saveData[78], saveData[79] };
                    byte hairColorRed = saveData[68];
                    byte hairColorGreen = saveData[69];
                    byte hairColorBlue = saveData[70];
                    byte clothingColorRed = saveData[22416];
                    byte clothingColorGreen = saveData[22417];
                    byte clothingColorBlue = saveData[22418];

                    /*Obviously change the value whenever you find its location
                    byte gender = saveData[0];
                    byte face = saveData[0];
                    byte feature1 = saveData[0];
                    byte feature2 = saveData[0];
                    byte feature1ColorRed = saveData[0];
                    byte feature1ColorGreen = saveData[0];
                    byte feature1ColorBlue = saveData[0];
                    byte feature2ColorRed = saveData[0];
                    byte feature2ColorGreen = saveData[0];
                    byte feature2ColorBlue = saveData[0];
                    byte skinColorRed = saveData[0];
                    byte skinColorGreen = saveData[0];
                    byte skinColorBlue = saveData[0];
                    */

                    //Convert playerName (player's name written in hex) to readable text, and update the player name textbox to it:
                    textBoxPlayerName.Text = System.Text.Encoding.UTF8.GetString(playerName);
                    //Because clothing is saved as a hex number between 00 and 03, it can be used directly to pick the index of the clothing comboBox
                    comboBoxClothing.SelectedIndex = clothing;
                    //Because data is stored in little endian, must reverse the byte[] when converting from byte[] to integer
                    Array.Reverse(zenny);
                    numericUpDownZenny.Text = BitConverter.ToInt32(zenny, 0).ToString();
                    Array.Reverse(resource);
                    numericUpDownResource.Text = BitConverter.ToInt32(resource, 0).ToString();
                    Array.Reverse(time);
                    numericUpDownTime.Text = BitConverter.ToInt32(time, 0).ToString();

                    //Changing hair color button to hair color selected in game
                    buttonColorClothing.BackColor = Color.FromArgb(clothingColorRed, clothingColorGreen, clothingColorBlue);
                    buttonColorHairstyle.BackColor = Color.FromArgb(hairColorRed, hairColorGreen, hairColorBlue);

                    //Create item box UI. Using UpdateItemBox() instead of createItemBox(), because they pracitically accomplish the same thing
                    //and createItemBox() has issues if trying to open another file (after already opening one)
                    //Also, opens itemBox UI if on Item Box tab, equipBox UI for Equipment Box tab, and nothing if on neither
                    if (tabControl1.SelectedTab == tabPage1) //Item Box
                    {
                        UpdateItemBox();
                    }

                    else if (tabControl1.SelectedTab == tabPage2) // Equipment Box
                    {
                        UpdateEquipBox();
                    }
                }

                //If the file fails the check for valid save file, give error
                else
                {
                    System.Windows.Forms.MessageBox.Show("There was an issue reading your save file! Please ensure the file is a valid MH3U save file and has no file extension. " +
                    "\n(For example, the file may be named \"user1\")");
                }
            }
        }

        //Function that constructs the buttons used for item placement 
        public void createItemBox()
        {   
            //Now time for the actual item box stuff
            var rowCount = 10;
            var columnCount = 10;

            this.tableLayoutPanel1.ColumnCount = columnCount;
            this.tableLayoutPanel1.RowCount = rowCount;

            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();

            for (int i = 0; i < columnCount; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / columnCount));
            }
            for (int i = 0; i < rowCount; i++)
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / rowCount));
            }

            //Variables to be used by the buttons
            int offset = 0; //Determines where each button starts to read/write data
            int itemBoxStart = 432; //Address of where in saveData the itembox actually starts
            //Essentially, every button will have an offset related to the data the button is supposed to interact with
            //The 1st button has an offset of 0 and will interact with 4 bytes of data (bytes 0 to 3). Therefore, 2nd button has an offset of 4 (bytes 4 to 7),
            //3rd button has an offset of 8 (bytes 8 to 11), etc.
            //When a button interacts with the save data, it will use the offset in tandem with the address of the first item in the box to 
            //determine which item in the save data it is supposed to interact with.

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    //Creating the buttons
                    var button = new Button();
                    //Buttons should be big enough to read what item it contains
                    button.Height = 70;
                    button.Width = 70;
                    button.Margin = new Padding(0, 0, 0, 0);
                    button.Name = string.Format("button_{0}{1}", i, j); // Buttons will be named from button_00 to button_99
                    button.Dock = DockStyle.Fill;
                    this.tableLayoutPanel1.Controls.Add(button, j, i);
                    this.tableLayoutPanel1.AutoSize = true;
                    this.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    this.DoubleBuffered = true;

                    byte[] itemData = new byte[4]; // Stores relevant item data for each button
                    int boxPage = comboBoxPage.SelectedIndex; // Stores what box page we're on
                    int pageOffset = boxPage * 400; // There are 400 bytes in one page, so this number will account for what page of the item box we should be on

                    //Save into itemData the block of bytes each button will use
                    for (int k = 0; k < 4; k++)
                    {
                        itemData[k] = saveData[itemBoxStart + offset + pageOffset + k];
                    }

                    //Creating variables to split the item data between the first two bytes (name of the item) and the last two (amount of the item)
                    //Wow, OK, I have to create it as a byte size of 4, because BitConverter doesn't work if the byte[] is smaller than 4
                    byte[] itemName = new byte[] { 00, 00, itemData[0], itemData[1] };
                    byte[] itemAmount = new byte[] { 00, 00, itemData[2], itemData[3] };

                    string itemDetails; // Initializing a string to hold the name/amount of each item in the box

                    //Now to turn itemName into string text. To do this, the hex data is first converted from little endian to big endian (via Reverse). 
                    //Then, the big endian hex data converted into decimal. Then, the decimal value is used as the index value to get from the item comboBox list.
                    // I.e., hex value = 000A -> decimal value = 10 -> index of comboBox at 10 = Mega Potion
                    Array.Reverse(itemName);
                    int itemNameInt = BitConverter.ToInt32(itemName, 0);
                    itemDetails = comboBoxItems.Items[itemNameInt].ToString();

                    //Thankfully, turning itemAmount into text is easier
                    Array.Reverse(itemAmount);
                    itemDetails += "\nx" + BitConverter.ToInt32(itemAmount, 0).ToString();

                    button.Text = itemDetails; // Each button will display the item in the box slot

                    //Creates a popup box when highlighting the button with your mouse. This box displays the item details as well, in case the item name is too long
                    toolTip1.SetToolTip(button, itemDetails);

                    //Giving all buttons the functionality to replace the specific part of saveData they are each responsible for
                    button.Click += (sender, EventArgs) => { itemBoxButtonClick(sender, EventArgs, saveData, itemData, button.Name); };

                    //Increase the offset for the next button
                    offset += 4;
                }
            }
        }

        //Quick and dirty way to update the entire item box layout (used for when changing between box pages and when changing between item/equipment box)
        void UpdateItemBox()
        {
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel1.Controls.Clear();
            createItemBox();
            tableLayoutPanel1.Visible = true;
        }

        //Functionality for when an item box button is clicked on
        public void itemBoxButtonClick(object sender, EventArgs e, byte[] saveData, byte[] itemData, string buttonName)
        {
            //Can't just pass offset/ itemboxstart from the button click event, since it only passes the final value (832), rather than the value for each button
            //Instead of figuring that out, I'll just do it an easier way, by using the name of the button (00,01,02,etc.)) to determine the offset
            int buttonIndex = Int32.Parse(buttonName.Substring(7));
            int offset = 4;
            int itemBoxStart = 432;
            int boxPage = comboBoxPage.SelectedIndex;
            int pageOffset = boxPage * 400; //there are 400 bytes in one page, so this number will account for what page of the item box we should be on
            int dataLocation = itemBoxStart + (buttonIndex * offset) + pageOffset;

            //Make sure the input fields aren't blank
            if (numericUpDownAmount.Text == "" || comboBoxItems.SelectedIndex < 0)
            {
                System.Windows.Forms.MessageBox.Show("Please ensure the item and item amount boxes are not blank. Additionally, ensure the item is spelled correctly " +
                    "(matches how it's spelled in the dropdown list).");
            }

            //Now that we made sure the values are correct, we can continue with the function
            else
            {
                //Take the item selected in the search bar + the item amount
                int item = comboBoxItems.SelectedIndex;
                int itemAmount = Int32.Parse(numericUpDownAmount.Text);

                //Using a function I created that takes an int variable and converts it into bytes. The number of *characters* in the byte[] output
                //Is equal to the string number given (in this case we want something like [01 02], which is 4 *characters* long)
                byte[] itemBytes = convertIntToBytes(item, "4");
                byte[] itemAmountBytes = convertIntToBytes(itemAmount, "4");

                //Then, add those 4 bytes into itemData
                for (int i = 0; i < 2; i++)
                {
                    itemData[i] = itemBytes[i];
                    itemData[i + 2] = itemAmountBytes[i];
                }

                //Replace the data in saveData with the new data in itemData
                for (int i = 0; i < 4; i++)
                {
                    saveData[dataLocation + i] = itemData[i];
                }

                //Updates the text on the clicked button to whatever was just inserted
                this.Controls.Find(buttonName, true).FirstOrDefault().Text = comboBoxItems.Text + "\nx" + itemAmount;

                //Updates the hint for the clicked button
                toolTip1.SetToolTip(this.Controls.Find(buttonName, true).FirstOrDefault(), comboBoxItems.Text + "\nx" + itemAmount);
            }
        }

        //Handles updating the box when changing the item in the box page combobox
        private void comboBoxPage_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //If Item Box tab is selected
            if (tabControl1.SelectedTab == tabPage1) //Item Box
            {
                UpdateItemBox();
            }

            //If Equipment Box tab is selected
            else if (tabControl1.SelectedTab == tabPage2) // Equipment Box
            {
                UpdateEquipBox();
            }

            // If "Player Character" or "Misc. Edits" tabs are selected, remove the ability to inject items/equipment
            else
            {
                tableLayoutPanel1.Controls.Clear();
            }
        }

        //Function that constructs the buttons used for equipment placement
        public void createEquipBox()
        {
            //A lot of this stuff is just copy/pasted from createItemBox()
            var rowCount = 10;
            var columnCount = 10;
            this.tableLayoutPanel1.ColumnCount = columnCount;
            this.tableLayoutPanel1.RowCount = rowCount;
            this.tableLayoutPanel1.ColumnStyles.Clear();
            this.tableLayoutPanel1.RowStyles.Clear();

            for (int i = 0; i < columnCount; i++)
            {
                this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100 / columnCount));
            }
            for (int i = 0; i < rowCount; i++)
            {
                this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100 / rowCount));
            }

            int offset = 0; //Determines where each button starts to read/write data
            int equipBoxStart = 4432; //Address of where in saveData the equip box actually starts

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    var button = new Button();
                    button.Height = 70;
                    button.Width = 70;
                    button.Margin = new Padding(0, 0, 0, 0);
                    button.Name = string.Format("button_{0}{1}", i, j);
                    button.Dock = DockStyle.Fill;
                    this.tableLayoutPanel1.Controls.Add(button, j, i);
                    this.tableLayoutPanel1.AutoSize = true;
                    this.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                    byte[] equipData = new byte[16]; // Stores relevant equip data for each button
                    int boxPage = comboBoxPage.SelectedIndex; // Stores what box page we're on
                    int pageOffset = boxPage * 1600; // There are 1600 bytes in one page, so this number will account for what page of the equip box we should be on

                    //Save into equipData the block of bytes each button will use
                    for (int k = 0; k < 16; k++)
                    {
                        equipData[k] = saveData[equipBoxStart + offset + pageOffset + k];
                    }

                    //Splitting up equipData into several variables. Need to pad each variable with [00 00] at the start, because BitConverter needs at least 4 bytes to work
                    byte[] equipType = new byte[] { 00, 00, 00, equipData[0] };
                    byte[] equipUpgrade = new byte[] { 00, 00, 00, equipData[1] };
                    byte[] equipName = new byte[] { 00, 00, equipData[2], equipData[3] };
                    byte[] talisSkill1 = new byte[] { 00, 00, 00, equipData[4] };
                    byte[] skill1Points = new byte[] { 00, 00, 00, equipData[5] };
                    byte[] talisSkill2 = new byte[] { 00, 00, 00, equipData[6] };
                    byte[] skill2Points = new byte[] { 00, 00, 00, equipData[7] };
                    byte[] equipDeco1 = new byte[] { 00, 00, equipData[8], equipData[9] };
                    byte[] equipDeco2 = new byte[] { 00, 00, equipData[10], equipData[11] };
                    byte[] equipDeco3 = new byte[] { 00, 00, equipData[12], equipData[13] };
                    byte[] equipDeco4 = new byte[] { 00, 00, equipData[14], equipData[15] };

                    string equipDetails; // Holds the information of each equip box item, for displaying purposes

                    //Because equipment is a little more complicated, first we'll determine what type of equipment it is. This will be done by
                    //using equipType to determine what kind of type it is, and then storing the related combobox into the variable "relatedComboBox" for processing
                    Array.Reverse(equipType);
                    int equipTypeInt = BitConverter.ToInt32(equipType, 0);

                    //Using a function that determines what type of equipment should be displayed using the reasoning above
                    var relatedComboBox = determineEquipmentTypeComboBox(equipTypeInt);

                    //Now to turn the equip name, upgrades, and slots into string text
                    Array.Reverse(equipName);
                    int equipNameInt = BitConverter.ToInt32(equipName, 0);
                    equipNameInt = readGlitchedWeapon(equipTypeInt, equipNameInt);

                    //Making a string solely for the button, since the full equipDetails has terrible legibility
                    string equipDetailsButtonLabel = "";

                    //If the user already hacked in some items, it might have an index that doesn't fit within the comboBoxes created. Because of this,
                    //we must first determine if the number (i.e. 1000) fits as an index, and if not, we label it as "Unknown Equipment"
                    try
                    {
                        equipDetails = relatedComboBox.Items[equipNameInt].ToString() + "\nType: " + determineUnknownEquipmentType(equipTypeInt);
                        equipDetailsButtonLabel = relatedComboBox.Items[equipNameInt].ToString();
                    }

                    catch 
                    {
                        equipDetails = "[Unknown] " + determineUnknownEquipmentType(equipTypeInt);
                        equipDetailsButtonLabel = "[Unknown] " + determineUnknownEquipmentType(equipTypeInt);
                    }

                    //Talisman data is read differently, so its equipDetails will be different
                    if (equipTypeInt == 6)
                    {
                        Array.Reverse(equipUpgrade);
                        equipDetails += "\nNumber of Slots: " + BitConverter.ToInt32(equipUpgrade, 0).ToString();
                        Array.Reverse(equipDeco1);
                        equipDetails += "\nSlot 1: " + comboBoxSlot1.Items[BitConverter.ToInt32(equipDeco1, 0)].ToString();
                        Array.Reverse(equipDeco2);
                        equipDetails += "\nSlot 2: " + comboBoxSlot2.Items[BitConverter.ToInt32(equipDeco2, 0)].ToString();
                        Array.Reverse(equipDeco3);
                        equipDetails += "\nSlot 3: " + comboBoxSlot3.Items[BitConverter.ToInt32(equipDeco3, 0)].ToString();
                        Array.Reverse(equipDeco4);
                        equipDetails += "\nSlot 4: " + comboBoxSlot4.Items[BitConverter.ToInt32(equipDeco4, 0)].ToString();
                        Array.Reverse(talisSkill1);
                        equipDetails += "\nTalisman Skill 1: " + comboBoxTalisSkill1.Items[BitConverter.ToInt32(talisSkill1, 0)].ToString();
                        Array.Reverse(skill1Points);
                        equipDetails += "\nSkill 1 Point(s): " + BitConverter.ToInt32(skill1Points, 0).ToString();
                        Array.Reverse(talisSkill2);
                        equipDetails += "\nTalisman Skill 2: " + comboBoxTalisSkill1.Items[BitConverter.ToInt32(talisSkill2, 0)].ToString();
                        Array.Reverse(skill2Points);
                        equipDetails += "\nSkill 2 Point(s): " + BitConverter.ToInt32(skill2Points, 0).ToString();
                    }

                    // Anything that isn't a talisman can have the same hint
                    else
                    {
                        Array.Reverse(equipUpgrade);
                        equipDetails += "\nUpgrade(s): " + BitConverter.ToInt32(equipUpgrade, 0).ToString();
                        Array.Reverse(equipDeco1);
                        equipDetails += "\nSlot 1: " + comboBoxEquipDeco.Items[BitConverter.ToInt32(equipDeco1, 0)].ToString();
                        Array.Reverse(equipDeco2);
                        equipDetails += "\nSlot 2: " + comboBoxEquipDeco.Items[BitConverter.ToInt32(equipDeco2, 0)].ToString();
                        Array.Reverse(equipDeco3);
                        equipDetails += "\nSlot 3: " + comboBoxEquipDeco.Items[BitConverter.ToInt32(equipDeco3, 0)].ToString();
                        Array.Reverse(equipDeco4);
                        equipDetails += "\nSlot 4: " + comboBoxEquipDeco.Items[BitConverter.ToInt32(equipDeco4, 0)].ToString();
                    }

                    //Each button will display the item in the box slot
                    button.Text = equipDetailsButtonLabel;

                    //Creates a popup box when highlighting the button with your mouse. This box displays extended details about the equipment
                    toolTip1.SetToolTip(button, equipDetails);

                    //Giving all buttons the functionality to replace the specific part of saveData they are each responsible for
                    button.Click += (sender, EventArgs) => { equipBoxButtonClick(sender, EventArgs, saveData, equipData, button.Name); };

                    //Increase the offset for the next button. Because all equipment are 16 bytes long, this offset is increased from 4 (for items) to 16 (for equipment)
                    offset += 16;

                }
            }
        }

        //Quick and dirty way to update the entire equipment box layout
        void UpdateEquipBox()
        {
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel1.Controls.Clear();
            createEquipBox();
            tableLayoutPanel1.Visible = true;
        }

        //Handles updating the equipment box when changing the tab from Item to Equipment
        private void formTabEquipmentClick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1) //Item Box
            {
                UpdateItemBox();
            }

            else if (tabControl1.SelectedTab == tabPage2) // Equipment Box
            {
                UpdateEquipBox();
            }
            
            else // If "Player Character" or "Misc. Edits" tabs are selected, remove the ability to inject items/equipment
            {
                tableLayoutPanel1.Controls.Clear();
            }
        }

        //Because each weapon type has its own combobox of names, this hides all of them (and in the next function, only the needed one becomes visible)
        private void hideComboBoxEquipNames()
        {
            comboBoxEquipHead.Visible = false;
            comboBoxEquipChest.Visible = false;
            comboBoxEquipArms.Visible = false;
            comboBoxEquipWaist.Visible = false;
            comboBoxEquipLegs.Visible = false;
            comboBoxEquipDeco.Visible = false;
            comboBoxEquipTalis.Visible = false;
            comboBoxEquipTalisSkill.Visible = false;
            comboBoxEquipGS.Visible = false;
            comboBoxEquipLS.Visible = false;
            comboBoxEquipSnS.Visible = false;
            comboBoxEquipDB.Visible = false;
            comboBoxEquipHammer.Visible = false;
            comboBoxEquipHH.Visible = false;
            comboBoxEquipLance.Visible = false;
            comboBoxEquipGL.Visible = false;
            comboBoxEquipSA.Visible = false;
            comboBoxEquipLB.Visible = false;
            comboBoxEquipHB.Visible = false;
            comboBoxEquipBow.Visible = false;
            numericUpDownUpgrade.Enabled = false;
            numericUpDownUpgrade.Value = 0;
            comboBoxTalisSkill1.Enabled = false;
            comboBoxTalisSkill1.SelectedIndex = 0;
            numericUpDownSkill1Points.Enabled = false;
            numericUpDownSkill1Points.Value = 0;
            comboBoxTalisSkill2.Enabled = false;
            comboBoxTalisSkill2.SelectedIndex = 0;
            numericUpDownSkill2Points.Enabled = false;
            numericUpDownSkill2Points.Value = 0;
        }

        //Update the equipment name combobox depending on what was selected from the equipment type combobox
        private void comboBoxEquipType_SelectedValueChanged(object sender, EventArgs e)
        {
            //Depending on what was chosen in comboBoxEquipType, update comboBoxEquipName to only include options of that type.
            //i.e. if "Great Swords" is chosen in comboBoxEquipType, comboBoxEquipName will list every Great Sword in the game
            //This is done by placing invisible comboBoxes over each other and then making them visible.
            switch (comboBoxEquipType.SelectedIndex)
            {
                case 0: // Head Armor
                    hideComboBoxEquipNames();
                    comboBoxEquipHead.Location = new Point(119, 43);
                    comboBoxEquipHead.Visible = true;
                    numericUpDownUpgrade.Enabled = true;
                    break;

                case 1: // Chest Armor
                    hideComboBoxEquipNames();
                    comboBoxEquipChest.Location = new Point(119, 43);
                    comboBoxEquipChest.Visible = true;
                    numericUpDownUpgrade.Enabled = true;
                    break;

                case 2: // Arm Armor
                    hideComboBoxEquipNames();
                    comboBoxEquipArms.Location = new Point(119, 43);
                    comboBoxEquipArms.Visible = true;
                    numericUpDownUpgrade.Enabled = true;
                    break;

                case 3: // Waist Armor
                    hideComboBoxEquipNames();
                    comboBoxEquipWaist.Location = new Point(119, 43);
                    comboBoxEquipWaist.Visible = true;
                    numericUpDownUpgrade.Enabled = true;
                    break;

                case 4: // Leg Armor
                    hideComboBoxEquipNames();
                    comboBoxEquipLegs.Location = new Point(119, 43);
                    comboBoxEquipLegs.Visible = true;
                    numericUpDownUpgrade.Enabled = true;
                    break;

                case 5: // Talismans
                    hideComboBoxEquipNames();
                    comboBoxEquipTalis.Location = new Point(119, 43);
                    comboBoxEquipTalis.Visible = true;
                    comboBoxTalisSkill1.Enabled = true;
                    numericUpDownSkill1Points.Enabled = true;
                    comboBoxTalisSkill2.Enabled = true;
                    numericUpDownSkill2Points.Enabled = true;
                    break;

                case 6: // Great Sword
                    hideComboBoxEquipNames();
                    comboBoxEquipGS.Location = new Point(119, 43);
                    comboBoxEquipGS.Visible = true;
                    break;

                case 7: // Long Sword
                    hideComboBoxEquipNames();
                    comboBoxEquipLS.Location = new Point(119, 43);
                    comboBoxEquipLS.Visible = true;
                    break;

                case 8: // Sword and Shield
                    hideComboBoxEquipNames();
                    comboBoxEquipSnS.Location = new Point(119, 43);
                    comboBoxEquipSnS.Visible = true;
                    break;

                case 9: // Dual Blades
                    hideComboBoxEquipNames();
                    comboBoxEquipDB.Location = new Point(119, 43);
                    comboBoxEquipDB.Visible = true;
                    break;

                case 10: // Hammer
                    hideComboBoxEquipNames();
                    comboBoxEquipHammer.Location = new Point(119, 43);
                    comboBoxEquipHammer.Visible = true;
                    break;

                case 11: // Hunting Horn
                    hideComboBoxEquipNames();
                    comboBoxEquipHH.Location = new Point(119, 43);
                    comboBoxEquipHH.Visible = true;
                    break;

                case 12: // Lance
                    hideComboBoxEquipNames();
                    comboBoxEquipLance.Location = new Point(119, 43);
                    comboBoxEquipLance.Visible = true;
                    break;

                case 13: // Gunlance
                    hideComboBoxEquipNames();
                    comboBoxEquipGL.Location = new Point(119, 43);
                    comboBoxEquipGL.Visible = true;
                    break;

                case 14: // Switch Axe
                    hideComboBoxEquipNames();
                    comboBoxEquipSA.Location = new Point(119, 43);
                    comboBoxEquipSA.Visible = true;
                    break;

                case 15: // Light Bowgun
                    hideComboBoxEquipNames();
                    comboBoxEquipLB.Location = new Point(119, 43);
                    comboBoxEquipLB.Visible = true;
                    break;

                case 16: // Heavy Bowgun
                    hideComboBoxEquipNames();
                    comboBoxEquipHB.Location = new Point(119, 43);
                    comboBoxEquipHB.Visible = true;
                    break;

                case 17: // Bow
                    hideComboBoxEquipNames();
                    comboBoxEquipBow.Location = new Point(119, 43);
                    comboBoxEquipBow.Visible = true;
                    break;

                case 18: // Decorations
                    hideComboBoxEquipNames();
                    comboBoxEquipDeco.Location = new Point(119, 43);
                    comboBoxEquipDeco.Visible = true;
                    break;
            }
        }

        //Function to determine the which comboBox to use based on the equipment type byte.
        //Making this just to keep createEquipBox() from getting even more bloated
        private ComboBox determineEquipmentTypeComboBox(int equipTypeInt)
        {
            
            var relatedComboBox = comboBoxEquipHead;

            switch (equipTypeInt)
            {
                case 0: //
                    relatedComboBox = comboBoxEquipEmpty; // Catches all types that start with 00, AKA an empty slot
                    break;

                case 1: // Chest Armor
                    relatedComboBox = comboBoxEquipChest;
                    break;

                case 2: // Arm Armor
                    relatedComboBox = comboBoxEquipArms;
                    break;

                case 3: // Waist Armor
                    relatedComboBox = comboBoxEquipWaist;
                    break;

                case 4: // Leg Armor
                    relatedComboBox = comboBoxEquipLegs;
                    break;

                case 5: // Head Armor
                    relatedComboBox = comboBoxEquipHead;
                    break;

                case 6: // Talismans
                    relatedComboBox = comboBoxEquipTalis;
                    break;

                case 7: // Great Sword
                    relatedComboBox = comboBoxEquipGS;
                    break;

                case 8: // Sword and Shield
                    relatedComboBox = comboBoxEquipSnS;
                    break;

                case 9: // Hammer
                    relatedComboBox = comboBoxEquipHammer;
                    break;

                case 10: // Lance
                    relatedComboBox = comboBoxEquipLance;
                    break;

                case 11: // Heavy Bowgun
                    relatedComboBox = comboBoxEquipHB;
                    break;

                // 12 doesn't exist. If you try to inject an item with a 0C type (12 in hex), it creates an empty/ glitched item

                case 13: // Light Bowgun
                    relatedComboBox = comboBoxEquipLB;
                    break;

                case 14: // Long Sword
                    relatedComboBox = comboBoxEquipLS;
                    break;

                case 15: // Switch Axe
                    relatedComboBox = comboBoxEquipSA;
                    break;

                case 16: // Gunlance
                    relatedComboBox = comboBoxEquipGL;
                    break;

                case 17: // Bow
                    relatedComboBox = comboBoxEquipBow;
                    break;

                case 18: // Dual Blades
                    relatedComboBox = comboBoxEquipDB;
                    break;

                case 19: // Hunting Horn
                    relatedComboBox = comboBoxEquipHH;
                    break;
            }

            return relatedComboBox;
        }

        //Function to determine the name of an unknown weapon. Doesn't do much other than help the user if they already hacked in equipment
        //that this program doesn't know about
        private String determineUnknownEquipmentType(int equipTypeInt)
        {
            switch (equipTypeInt)
            {
                case 0: // Empty
                    return "<Empty>";

                case 1: // Chest Armor
                    return "Chest Armor";

                case 2: // Arm Armor
                    return "Arm Armor";

                case 3: // Waist Armor
                    return "Waist Armor";

                case 4: // Leg Armor
                    return "Leg Armor";

                case 5: // Head Armor
                    return "Head Armor";

                case 6: // Talismans
                    return "Talisman";

                case 7: // Great Sword
                    return "Great Sword";

                case 8: // Sword and Shield
                    return "Sword and Shield";

                case 9: // Hammer
                    return "Hammer";

                case 10: // Lance
                    return "Lance";

                case 11: // Heavy Bowgun
                    return "Heavy Bowgun";

                // 12 doesn't exist. If you try to inject an item with a 0C type (12 in hex), it creates an empty/ glitched item

                case 13: // Light Bowgun
                    return "Light Bowgun";

                case 14: // Long Sword
                    return "Long Sword";

                case 15: // Switch Axe
                    return "Switch Axe";

                case 16: // Gunlance
                    return "Gunlance";

                case 17: // Bow
                    return "Bow";

                case 18: // Dual Blades
                    return "Dual Blades";

                case 19: // Hunting Horn
                    return "Hunting Horn";
            }

            //Another catch-all in case (for some reason) the equipType is invalid
            return "Undefined Equipment";
        }

        public void equipBoxButtonClick(object sender, EventArgs e, byte[] saveData, byte[] itemData, string buttonName)
        {
            //A lot of this stuff is just copy / pasted from itemBoxButtonClick()
            int buttonIndex = Int32.Parse(buttonName.Substring(7));
            int boxPage = comboBoxPage.SelectedIndex;
            int offset = 16;
            int equipBoxStart = 4432; // Equip box starts after item box ends
            int pageOffset = boxPage * 1600; // There are 1600 bytes in one page of equipment box (16 bytes per equipment * 100)
            int dataLocation = equipBoxStart + (buttonIndex * offset) + pageOffset;

            //Make sure the input fields aren't blank
            if (comboBoxEquipType.Text == "" || comboBoxEquipType.SelectedIndex < 0)
            {
                System.Windows.Forms.MessageBox.Show("Please ensure the equipment type and equipment name boxes are not blank. " +
                    "Additionally, ensure the item is spelled correctly (matches how it's spelled in the dropdown list).");
            }

            //Now that we made sure the values are correct, we can continue with the function
            else
            {
                //Take the equipment selected in the type, upgrade and slot bars
                int equipType = comboBoxEquipType.SelectedIndex;
                int equipUpgrades = Int32.Parse(numericUpDownUpgrade.Text);
                int slot1 = comboBoxSlot1.SelectedIndex;
                int slot2 = comboBoxSlot2.SelectedIndex;
                int slot3 = comboBoxSlot3.SelectedIndex;
                int slot4 = comboBoxSlot4.SelectedIndex;

                //Finding the name is much more annoying, though. Using the value of equipType, it needs to reference the related equipName comboBox 
                //(i.e., equipNameGS or equipNameHead, etc.). Though I've made similar functions to this already, they aren't able to return the info
                //I need for this specifically, so I have to do it the old fashioned way.
                //Oh, and additionally, I can't just use equipType taken from the SelectedIndex, since the index doesn't match the bytes (index 05 is
                //talismans in the comboBox, but actually head armor in the data). Plus, 12 (or more accurately, 0C) is unused entirely, so I can't just
                //re-order the options in the comboBox. So, I'll just reassign the equipType with the value it should be in each case below
                int equipName = 0;
                string equipNameText = "";
                switch (equipType)
                {
                    case 0: // Head Armor
                        equipName = comboBoxEquipHead.SelectedIndex;
                        equipType = 5;
                        equipNameText = comboBoxEquipHead.Text;
                        break;

                    case 1: // Chest Armor
                        equipName = comboBoxEquipChest.SelectedIndex;
                        equipType = 1;
                        equipNameText = comboBoxEquipChest.Text;
                        break;

                    case 2: // Arm Armor
                        equipName = comboBoxEquipArms.SelectedIndex;
                        equipType = 2;
                        equipNameText = comboBoxEquipArms.Text;
                        break;

                    case 3: // Waist Armor
                        equipName = comboBoxEquipWaist.SelectedIndex;
                        equipType = 3;
                        equipNameText = comboBoxEquipWaist.Text;
                        break;

                    case 4: // Leg Armor
                        equipName = comboBoxEquipLegs.SelectedIndex;
                        equipType = 4;
                        equipNameText = comboBoxEquipLegs.Text;
                        break;

                    case 5: // Talismans
                        equipName = comboBoxEquipTalis.SelectedIndex;
                        equipType = 6;
                        equipNameText = comboBoxEquipTalis.Text;
                        break;

                    case 6: // Great Sword 
                        equipType = 7;
                        equipName = writeGlitchedWeapon(equipType, comboBoxEquipGS.SelectedIndex);
                        equipNameText = comboBoxEquipGS.Text;
                        break;

                    case 7: // Long Sword
                        equipName = comboBoxEquipLS.SelectedIndex;
                        equipType = 14;
                        equipNameText = comboBoxEquipLS.Text;
                        break;

                    case 8: // Sword and Shield
                        equipType = 8;
                        equipName = writeGlitchedWeapon(equipType, comboBoxEquipSnS.SelectedIndex);
                        equipNameText = comboBoxEquipSnS.Text;
                        break;

                    case 9: // Dual Blades
                        equipName = comboBoxEquipDB.SelectedIndex;
                        equipType = 18;
                        equipNameText = comboBoxEquipDB.Text;
                        break;

                    case 10: // Hammer
                        equipType = 9;
                        equipName = writeGlitchedWeapon(equipType, comboBoxEquipHammer.SelectedIndex);
                        equipNameText = comboBoxEquipHammer.Text;
                        break;

                    case 11: // Hunting Horn
                        equipName = comboBoxEquipHH.SelectedIndex;
                        equipType = 19;
                        equipNameText = comboBoxEquipHH.Text;
                        break;

                    case 12: // Lance
                        equipType = 10;
                        equipName = writeGlitchedWeapon(equipType, comboBoxEquipLance.SelectedIndex);
                        equipNameText = comboBoxEquipLance.Text;
                        break;

                    case 13: // Gunlance
                        equipType = 16;
                        equipName = writeGlitchedWeapon(equipType, comboBoxEquipGL.SelectedIndex);
                        equipNameText = comboBoxEquipGL.Text;
                        break;

                    case 14: // Switch Axe
                        equipType = 15;
                        equipName = writeGlitchedWeapon(equipType, comboBoxEquipSA.SelectedIndex);
                        equipNameText = comboBoxEquipSA.Text;
                        break;

                    case 15: // Light Bowgun
                        equipName = comboBoxEquipLB.SelectedIndex;
                        equipType = 13;
                        equipNameText = comboBoxEquipLB.Text;
                        break;

                    case 16: // Heavy Bowgun
                        equipName = comboBoxEquipHB.SelectedIndex;
                        equipType = 11;
                        equipNameText = comboBoxEquipHB.Text;
                        break;

                    case 17: // Bow
                        equipType = 17;
                        equipName = writeGlitchedWeapon(equipType, comboBoxEquipBow.SelectedIndex);
                        equipNameText = comboBoxEquipBow.Text;
                        break;
                }

                //Using a function I created that takes an int variable and converts it into bytes. The number of *characters* in the byte[] output
                //Is equal to the string number given (in this case we want something like [01 02], which is 4 *characters* long)
                byte[] equipTypeBytes = convertIntToBytes(equipType, "4");
                byte[] equipNameBytes = convertIntToBytes(equipName, "4");
                byte[] equipUpgradesBytes = convertIntToBytes(equipUpgrades, "4");
                byte[] slot1Bytes = convertIntToBytes(slot1, "4");
                byte[] slot2Bytes = convertIntToBytes(slot2, "4");
                byte[] slot3Bytes = convertIntToBytes(slot3, "4");
                byte[] slot4Bytes = convertIntToBytes(slot4, "4");

                byte[] equipData = new byte[] { equipTypeBytes[1], equipUpgradesBytes[1], equipNameBytes[0], equipNameBytes[1], 00, 00, 00, 00, 
                    slot1Bytes[0], slot1Bytes[1], slot2Bytes[0], slot2Bytes[1], slot3Bytes[0], slot3Bytes[1], slot4Bytes[0], slot4Bytes[1]};

                //Talismans are different than other equipment. Instead of upgrades, that part of the data is used to say how many decoration slots it has.
                //Additionally, the four "00"s in equipData are populated with talisman skills only
                int slotNumber = 4; // Talismans can have any number of decoration slots (0 to 4), but for convenience, I'll set every talisman to 4 deco slots
                if (equipType == 6) // For talismans only
                {
                    byte[] slotNumberBytes = convertIntToBytes(slotNumber, "4");
                    byte[] talisSkill1 = convertIntToBytes(comboBoxTalisSkill1.SelectedIndex, "2");
                    byte[] talisSkill2 = convertIntToBytes(comboBoxTalisSkill2.SelectedIndex, "2");
                    byte[] skill1Points = convertIntToBytes((int)numericUpDownSkill1Points.Value, "2");
                    byte[] skill2Points = convertIntToBytes((int)numericUpDownSkill2Points.Value, "2");

                    equipData = new byte[] { equipTypeBytes[1], slotNumberBytes[1], equipNameBytes[0], equipNameBytes[1], talisSkill1[0], 
                        skill1Points[0], talisSkill2[0], skill2Points[0], slot1Bytes[0], slot1Bytes[1], slot2Bytes[0], slot2Bytes[1], slot3Bytes[0], 
                        slot3Bytes[1], slot4Bytes[0], slot4Bytes[1]};
                }

                //Replace the data in saveData with the new data in itemData
                for (int i = 0; i < 16; i++)
                {
                    saveData[dataLocation + i] = equipData[i];
                }

                //Updates the text on the clicked button to whatever was just inserted
                this.Controls.Find(buttonName, true).FirstOrDefault().Text = equipNameText;

                //Updates the hint for the clicked button
                toolTip1.SetToolTip(this.Controls.Find(buttonName, true).FirstOrDefault(), equipNameText + "\nType: " + comboBoxEquipType.Text + 
                    "\nUpgrade(s): " + numericUpDownUpgrade.Text + "\nSlot 1: " + comboBoxSlot1.Text + "\nSlot 2: " + comboBoxSlot2.Text + "\nSlot 3: " + 
                    comboBoxSlot3.Text + "\nSlot 4: " + comboBoxSlot4.Text);

                //Again, talisman is different, so modifying the hint for them
                if (equipType == 6)
                {
                    toolTip1.SetToolTip(this.Controls.Find(buttonName, true).FirstOrDefault(), equipNameText + "\nType: " + comboBoxEquipType.Text +
                    "\nNumber of Slots: " + slotNumber + "\nSlot 1: " + comboBoxSlot1.Text + "\nSlot 2: " + comboBoxSlot2.Text + "\nSlot 3: " +
                    comboBoxSlot3.Text + "\nSlot 4: " + comboBoxSlot4.Text + "\nTalisman Skill 1: " + comboBoxTalisSkill1.Text + "\nSkill 1 Point(s): " + 
                    numericUpDownSkill1Points.Text + "\nTalisman Skill 2: " + comboBoxTalisSkill2.Text + "\nSkill 2 Point(s): " + numericUpDownSkill2Points.Text);
                }
            }
        }

        //The last value of most weapon comboBoxes are glitched weapons found far later into the hex address
        //(i.e., for Hammer, the 2nd last weapon has address [00 88] and the last weapon has address [0F FF].
        //This function accounts for this, associating the last comboBox item with its intended hex address value)
        private int readGlitchedWeapon(int equipTypeInt, int equipNameInt) 
        {
            //Only certain weapons necessary
            switch (equipTypeInt)
            {
                case 7: // Great Sword
                    if (equipNameInt == 4094) { equipNameInt = 178; } // If 4094 (from save file), then, use 178 (for comboBox)
                    break;

                case 8: // Sword and Shield
                    if (equipNameInt == 4094) { equipNameInt = 142; }
                    break;

                case 9: // Hammer
                    if (equipNameInt == 4095) { equipNameInt = 137; }
                    break;

                case 10: // Lance
                    if (equipNameInt == 4095) { equipNameInt = 149; }
                    break;

                case 15: // Switch Axe
                    if (equipNameInt == 256) { equipNameInt = 117; }
                    break;

                case 16: // Gunlance
                    if (equipNameInt == 256) { equipNameInt = 120; }
                    break;

                case 17: // Bow
                    if (equipNameInt == 4095) { equipNameInt = 118; }
                    break;
            }

            return equipNameInt;
        }

        //Same as the above function, but in reverse (if last weapon is chosen in comboBox, changes value to the correct one)
        private int writeGlitchedWeapon(int equipType, int equipNameIndex) 
        {
            //Only certain weapons necessary
            switch (equipType)
            {
                case 7: // Great Sword
                    if (equipNameIndex == 178) { equipNameIndex = 4094; } // If 178 (from comboBox), then, use 4094 (for save file)
                    break; 

                case 8: // Sword and Shield
                    if (equipNameIndex == 142) { equipNameIndex = 4094; }
                    break;

                case 9: // Hammer
                    if (equipNameIndex == 137) { equipNameIndex = 4095; }
                    break;

                case 10: // Lance
                    if (equipNameIndex == 149) { equipNameIndex = 4095; }
                    break;

                case 15: // Switch Axe
                    if (equipNameIndex == 117) { equipNameIndex = 256; }
                    break;

                case 16: // Gunlance
                    if (equipNameIndex == 120) { equipNameIndex = 256; }
                    break;

                case 17: // Bow
                    if (equipNameIndex == 118) { equipNameIndex = 4095; }
                    break;
            }

            return equipNameIndex;
        }

        //Function that saves all other fields in the application (zenny, res points, etc.). This function is called when the file is saved
        private void saveAllFields()
        {
            //Setting the address for all data
            int zennyAddress = 72;
            int resourceAddress = 23376;
            int timePlayedAddress = 76;
            int playerNameAddress = 43;
            int clothingAddress = 206;
            int clothingColorAddress = 22416;
            int hairstyleColorAddress = 68;

            //Getting the byte[] values for all necessary data
            byte[] zenny = convertIntToBytes((int)numericUpDownZenny.Value, "8");
            byte[] resource = convertIntToBytes((int)numericUpDownResource.Value, "8");
            byte[] timePlayed = convertIntToBytes((int)numericUpDownTime.Value, "8");
            byte[] playerName = new byte[24];
            byte[] clothing = convertIntToBytes(comboBoxClothing.SelectedIndex, "2");
            byte[] clothingColor = convertIntToBytes(buttonColorClothing.BackColor.ToArgb(),"8");
            byte[] hairstyleColor = convertIntToBytes(buttonColorHairstyle.BackColor.ToArgb(), "8");

            //Converting player name to byte[] is more complicated though
            char[] playerNameChar = new char[24];

            //Convert player name from text box into character array
            for(int i = 0; i < textBoxPlayerName.TextLength; i++)
            {
                playerNameChar[i] = textBoxPlayerName.Text[i];
            }

            //Then convert each character into a byte and store it into playerName
            for (int i = 0; i < 24; i++)
            {
                playerName[i] += Convert.ToByte(playerNameChar[i]);
            }

            //Saving the data into the file
            for (int i = 0; i < 4; i++) { saveData[zennyAddress + i] = zenny[i]; }
            for (int i = 0; i < 4; i++) { saveData[resourceAddress + i] = resource[i]; }
            for (int i = 0; i < 4; i++) { saveData[timePlayedAddress + i] = timePlayed[i]; }
            for (int i = 0; i < 24; i++) { saveData[playerNameAddress + i] = playerName[i]; }
            saveData[clothingAddress] = clothing[0];
            for (int i = 0; i < 3; i++) { saveData[clothingColorAddress + i] = clothingColor[i + 1]; } // ARGB code starts with alpha, which isn't used by the game, so must skip it
            for (int i = 0; i < 3; i++) { saveData[hairstyleColorAddress + i] = hairstyleColor[i + 1]; } // Same as above

        }

        //Creating a function that turns a int into a byte[]. bytelength is a string that determines how many *characters* the returned byte[] should be
        //I.e. [01 02] is 4 *characters*, [01 02 03 04] is 8 *characters*
        private byte[] convertIntToBytes(int value, string bytelength)
        {
            //Must validate the values (must be between -2147483648 and 2147483647)
            if (value > 2147483647) { value = 2147483647; }
            else if (value < -2147483648) { value = -2147483648; }

            String valueHex = value.ToString("X" + bytelength);
            SoapHexBinary valueSoap = SoapHexBinary.Parse(valueHex);
            byte[] valueBytes = valueSoap.Value;

            return valueBytes;
        }

        //When File > Save File is clicked
        private void saveFile_Click(object sender, EventArgs e)
        {
            saveAllFields();
            File.WriteAllBytes(saveFile, saveData);
        }

        //When File > Save As... is clicked
        private void saveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Title = "Save As...";
            sd.Filter = "All files (*.*)|*.*";
            sd.ShowDialog();
            saveAllFields();

            //Try-catch to prevent program from crashing if user cancels from the Save As pop-up window
            try
            {
                File.WriteAllBytes(sd.FileName.ToString(), saveData);
            }

            catch { }
        }
    }
}
