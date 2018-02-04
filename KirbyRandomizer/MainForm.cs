using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KirbyRandomizer
{
    public partial class MainForm : Form
    {
        uint hitboxPhysStart = 0x081E17;
        uint hitboxPhysEnd = 0x081F85;
        uint hitboxProjStart = 0x08290E;
        uint hitboxProjEnd = 0x082950;

        List<int> elementNormal = new List<int>(){ 0x00, 0x01, 0x02, 0x03 };
        List<int> elementSharp = new List<int>() { 0x04, 0x05, 0x06, 0x07 };
        List<int> elementFire = new List<int>() { 0x08, 0x09, 0x0A, 0x0B };
        List<int> elementElectric = new List<int>() { 0x0C, 0x0D, 0x0E, 0x0F };
        List<int> elementIce = new List<int>() { 0x10, 0x11, 0x12, 0x13 };
        List<int> elementNormal2 = new List<int>() { 0x14, 0x15, 0x16, 0x17 };

        uint enemyAbilityStart = 0x10426B;
        uint enemyAbilityEnd = 0x1042A5;
        uint miniBossAbilityStart = 0x1042AB;
        uint miniBossAbilityEnd = 0x1042B1;
        uint bossAbilityStart = 0x1042BA;
        uint bossAbilityEnd = 0x1042CB;
        uint regionOffset = 0xEE;
        uint USid = 0xF4;
        uint JPid = 0xCF;
        uint region = 0x0;

        Random rng = new Random();

        public MainForm()
        {
            InitializeComponent();
        }

        private void loadMINT_Click(object sender, EventArgs e)
        {
            OpenFileDialog readROM = new OpenFileDialog();
            readROM.DefaultExt = ".smc";
            readROM.AddExtension = true;
            readROM.Filter = "SNES SMC ROM Files|*.smc|All Files|*";
            if (readROM.ShowDialog() == DialogResult.OK)
            {
                filePath.Text = readROM.FileName;
                randSettingsGroup.Enabled = true;
                byte[] fileData = File.ReadAllBytes(readROM.FileName);
                if (fileData[regionOffset] == USid)
                {
                    romRegion.Text = "ROM Region: NTSC";
                }
                if (fileData[regionOffset] == JPid)
                {
                    romRegion.Text = "ROM Region: JPN";
                }
                region = fileData[regionOffset];
            }
        }

        private void randElements_CheckedChanged(object sender, EventArgs e)
        {
            if (randElements.Checked)
            {
                randElementsEach.Enabled = true;
                randOneElement.Enabled = true;
                randElementsHitboxes.Enabled = true;
                if (!randKB.Checked && !randEnemies.Checked)
                {
                    randomize.Enabled = true;
                }
            }
            if (!randElements.Checked)
            {
                randElementsEach.Enabled = false;
                randOneElement.Enabled = false;
                randElementsHitboxes.Enabled = false;
                if (!randKB.Checked && !randEnemies.Checked)
                {
                    randomize.Enabled = false;
                }
            }
        }

        private void randEnemies_CheckedChanged(object sender, EventArgs e)
        {
            if (randEnemies.Checked)
            {
                includeMinorEnemies.Enabled = true;
                randBossAbilities.Enabled = true;
                randMiniBossAbilities.Enabled = true;
                if (!randKB.Checked && !randElements.Checked)
                {
                    randomize.Enabled = true;
                }
            }
            if (!randEnemies.Checked)
            {
                includeMinorEnemies.Enabled = false;
                randBossAbilities.Enabled = false;
                randMiniBossAbilities.Enabled = false;
                if (!randKB.Checked && !randElements.Checked)
                {
                    randomize.Enabled = false;
                }
            }
        }

        private void randomize_Click(object sender, EventArgs e)
        {
            byte[] ROMdata = File.ReadAllBytes(filePath.Text);
            //Randomize Enemies
            if (randEnemies.Checked)
            {
                if (randomizeAbilities(ROMdata) != null) {
                    ROMdata = randomizeAbilities(ROMdata);
                }
                else
                {
                    MessageBox.Show("Error: Seed was not in correct format.", "Kirby Super Star Randomizer", MessageBoxButtons.OK);
                    return;
                }
            }
            //Randomize Elements
            if (randElements.Checked)
            {
                if (RandomizeHitboxElements(ROMdata) != null)
                {
                    ROMdata = RandomizeHitboxElements(ROMdata);
                }
                else
                {
                    MessageBox.Show("Error: Seed was not in correct format.", "Kirby Super Star Randomizer", MessageBoxButtons.OK);
                    return;
                }
            }
            //Overwrite ROM
            if (overwriteROM.Checked)
            {
                File.WriteAllBytes(filePath.Text, ROMdata);
                MessageBox.Show($"Successfully randomized Kirby Super Star ROM!\nFile written to:\n{filePath.Text}", "Kirby Super Star Randomizer", MessageBoxButtons.OK);
            }
            //Write to new file
            if (!overwriteROM.Checked)
            {
                string[] path = filePath.Text.Split('\\');
                path[path.Length - 1] = path[path.Length - 1].Replace(".smc", " Randomized.smc");
                string newFile = string.Join("\\", path);
                File.WriteAllBytes(newFile, ROMdata);
                MessageBox.Show($"Successfully randomized Kirby Super Star ROM!\nFile written to:\n{newFile}", "Kirby Super Star Randomizer");
            }
        }

        public byte[] randomizeAbilities(byte[] data)
        {
            //Seed stuff
            if (randSeed.Text != "")
            {
                if (int.TryParse(randSeed.Text, out int result))
                {
                    rng = new Random(result);
                }
                else
                {
                    byte[] end = null;
                    return end;
                }
            }
            //Enemies
            if (includeMinorEnemies.Checked)
            {
                for (uint i = enemyAbilityStart; i <= enemyAbilityEnd; i++)
                {
                    data[i] = byte.Parse(rng.Next(0, 24).ToString());
                }
            }
            if (!includeMinorEnemies.Checked)
            {
                List<int> randomAbilityResults = new List<int>();
                List<int> abilityCount = new List<int>()
                {
                    0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                };
                byte[] origData = data;
                bool ResultOK = false;
                while (!ResultOK)
                {
                    data = origData;
                    rng = new Random();
                    abilityCount = new List<int>()
                    {
                        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
                    };
                    randomAbilityResults.Clear();
                    //Randomize Abilities
                    for (uint i = 0; i <= 58; i++)
                    {
                        int randomAbility = rng.Next(0, 24);
                        if (data[enemyAbilityStart + i] != 0x00)
                        {
                            data[enemyAbilityStart + i] = byte.Parse(randomAbility.ToString());
                        }
                        randomAbilityResults.Add(randomAbility);
                    }
                    //Check randomization
                    if (randSeed.Text == "")
                    {
                        for (int i = 1; i < randomAbilityResults.Count; i++)
                        {
                            abilityCount[randomAbilityResults[i]] = abilityCount[randomAbilityResults[i]] + 1;
                        }
                        if (!abilityCount.Contains(0))
                        {
                            ResultOK = true;
                        }
                        else
                        {
                            ResultOK = false;
                        }
                        //debug stuff
                        if (ResultOK == false)
                        {
                            Console.WriteLine("Randomization Check: NOT OK");
                        }
                        if (ResultOK == true)
                        {
                            Console.WriteLine("Randomization Check: OK!");
                        }
                    }
                    else
                    {
                        ResultOK = true;
                    }
                }
            }
            //Mini-Bosses
            if (randMiniBossAbilities.Checked)
            {
                for (uint i = miniBossAbilityStart; i <= miniBossAbilityEnd; i++)
                {
                    data[i] = byte.Parse(rng.Next(0, 24).ToString());
                }
            }
            //Bosses
            if (randMiniBossAbilities.Checked)
            {
                for (uint i = bossAbilityStart; i <= bossAbilityEnd; i++)
                {
                    data[i] = byte.Parse(rng.Next(0, 24).ToString());
                }
            }
            return data;
        }

        public byte[] RandomizeHitboxElements(byte[] data)
        {
            //Seed stuff
            if (randSeed.Text != "")
            {
                if (int.TryParse(randSeed.Text, out int result))
                {
                    rng = new Random(result);
                }
                else
                {
                    byte[] end = null;
                    return end;
                }
            }
            //Randomize each Copy Ability
            if (randOneElement.Checked)
            {

            }
            //Randomize each attack
            if (randElementsEach.Checked)
            {

            }
            //Randomize each hitbox
            if (randElementsHitboxes.Checked)
            {
                for (uint i = hitboxPhysStart; i <= hitboxPhysEnd; i++)
                {
                    int element = rng.Next(0, 5);
                    //Rolling Normal
                    if (element == 0)
                    {
                        if (elementSharp.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal[elementSharp.IndexOf(data[i])].ToString());
                        }
                        if (elementFire.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal[elementFire.IndexOf(data[i])].ToString());
                        }
                        if (elementElectric.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal[elementElectric.IndexOf(data[i])].ToString());
                        }
                        if (elementIce.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal[elementIce.IndexOf(data[i])].ToString());
                        }
                        if (elementNormal2.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal[elementNormal2.IndexOf(data[i])].ToString());
                        }
                    }
                    //Rolling Sharp
                    if (element == 1)
                    {
                        if (elementNormal.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementSharp[elementNormal.IndexOf(data[i])].ToString());
                        }
                        if (elementFire.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementSharp[elementFire.IndexOf(data[i])].ToString());
                        }
                        if (elementElectric.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementSharp[elementElectric.IndexOf(data[i])].ToString());
                        }
                        if (elementIce.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementSharp[elementIce.IndexOf(data[i])].ToString());
                        }
                        if (elementNormal2.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementSharp[elementNormal2.IndexOf(data[i])].ToString());
                        }
                    }
                    //Rolling Fire
                    if (element == 2)
                    {
                        if (elementNormal.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementFire[elementNormal.IndexOf(data[i])].ToString());
                        }
                        if (elementSharp.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementFire[elementSharp.IndexOf(data[i])].ToString());
                        }
                        if (elementElectric.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementFire[elementElectric.IndexOf(data[i])].ToString());
                        }
                        if (elementIce.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementFire[elementIce.IndexOf(data[i])].ToString());
                        }
                        if (elementNormal2.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementFire[elementNormal2.IndexOf(data[i])].ToString());
                        }
                    }
                    //Rolling Electric
                    if (element == 3)
                    {
                        if (elementNormal.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementElectric[elementNormal.IndexOf(data[i])].ToString());
                        }
                        if (elementSharp.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementElectric[elementSharp.IndexOf(data[i])].ToString());
                        }
                        if (elementFire.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementElectric[elementFire.IndexOf(data[i])].ToString());
                        }
                        if (elementIce.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementElectric[elementIce.IndexOf(data[i])].ToString());
                        }
                        if (elementNormal2.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementElectric[elementNormal2.IndexOf(data[i])].ToString());
                        }
                    }
                    //Rolling Ice
                    if (element == 4)
                    {
                        if (elementNormal.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementIce[elementNormal.IndexOf(data[i])].ToString());
                        }
                        if (elementSharp.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementIce[elementSharp.IndexOf(data[i])].ToString());
                        }
                        if (elementFire.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementIce[elementFire.IndexOf(data[i])].ToString());
                        }
                        if (elementElectric.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementIce[elementElectric.IndexOf(data[i])].ToString());
                        }
                        if (elementNormal2.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementIce[elementNormal2.IndexOf(data[i])].ToString());
                        }
                    }
                    //Rolling Normal2
                    if (element == 5)
                    {
                        if (elementNormal.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal2[elementNormal.IndexOf(data[i])].ToString());
                        }
                        if (elementSharp.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal2[elementSharp.IndexOf(data[i])].ToString());
                        }
                        if (elementFire.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal2[elementFire.IndexOf(data[i])].ToString());
                        }
                        if (elementElectric.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal2[elementElectric.IndexOf(data[i])].ToString());
                        }
                        if (elementIce.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal2[elementIce.IndexOf(data[i])].ToString());
                        }
                    }
                }
                for (uint i = hitboxProjStart; i <= hitboxProjEnd; i++)
                {
                    int element = rng.Next(0, 5);
                    //Rolling Normal
                    if (element == 0)
                    {
                        if (elementSharp.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal[elementSharp.IndexOf(data[i])].ToString());
                        }
                        if (elementFire.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal[elementFire.IndexOf(data[i])].ToString());
                        }
                        if (elementElectric.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal[elementElectric.IndexOf(data[i])].ToString());
                        }
                        if (elementIce.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal[elementIce.IndexOf(data[i])].ToString());
                        }
                        if (elementNormal2.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal[elementNormal2.IndexOf(data[i])].ToString());
                        }
                    }
                    //Rolling Sharp
                    if (element == 1)
                    {
                        if (elementNormal.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementSharp[elementNormal.IndexOf(data[i])].ToString());
                        }
                        if (elementFire.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementSharp[elementFire.IndexOf(data[i])].ToString());
                        }
                        if (elementElectric.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementSharp[elementElectric.IndexOf(data[i])].ToString());
                        }
                        if (elementIce.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementSharp[elementIce.IndexOf(data[i])].ToString());
                        }
                        if (elementNormal2.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementSharp[elementNormal2.IndexOf(data[i])].ToString());
                        }
                    }
                    //Rolling Fire
                    if (element == 2)
                    {
                        if (elementNormal.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementFire[elementNormal.IndexOf(data[i])].ToString());
                        }
                        if (elementSharp.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementFire[elementSharp.IndexOf(data[i])].ToString());
                        }
                        if (elementElectric.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementFire[elementElectric.IndexOf(data[i])].ToString());
                        }
                        if (elementIce.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementFire[elementIce.IndexOf(data[i])].ToString());
                        }
                        if (elementNormal2.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementFire[elementNormal2.IndexOf(data[i])].ToString());
                        }
                    }
                    //Rolling Electric
                    if (element == 3)
                    {
                        if (elementNormal.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementElectric[elementNormal.IndexOf(data[i])].ToString());
                        }
                        if (elementSharp.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementElectric[elementSharp.IndexOf(data[i])].ToString());
                        }
                        if (elementFire.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementElectric[elementFire.IndexOf(data[i])].ToString());
                        }
                        if (elementIce.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementElectric[elementIce.IndexOf(data[i])].ToString());
                        }
                        if (elementNormal2.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementElectric[elementNormal2.IndexOf(data[i])].ToString());
                        }
                    }
                    //Rolling Ice
                    if (element == 4)
                    {
                        if (elementNormal.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementIce[elementNormal.IndexOf(data[i])].ToString());
                        }
                        if (elementSharp.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementIce[elementSharp.IndexOf(data[i])].ToString());
                        }
                        if (elementFire.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementIce[elementFire.IndexOf(data[i])].ToString());
                        }
                        if (elementElectric.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementIce[elementElectric.IndexOf(data[i])].ToString());
                        }
                        if (elementNormal2.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementIce[elementNormal2.IndexOf(data[i])].ToString());
                        }
                    }
                    //Rolling Normal2
                    if (element == 5)
                    {
                        if (elementNormal.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal2[elementNormal.IndexOf(data[i])].ToString());
                        }
                        if (elementSharp.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal2[elementSharp.IndexOf(data[i])].ToString());
                        }
                        if (elementFire.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal2[elementFire.IndexOf(data[i])].ToString());
                        }
                        if (elementElectric.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal2[elementElectric.IndexOf(data[i])].ToString());
                        }
                        if (elementIce.Contains(data[i]))
                        {
                            data[i] = byte.Parse(elementNormal2[elementIce.IndexOf(data[i])].ToString());
                        }
                    }
                }
            }
            return data;
        }

        public byte[] RandomizeHitboxKB(byte[] data)
        {
            return data;
        }

        private void randKB_CheckedChanged(object sender, EventArgs e)
        {
            if (randKB.Checked)
            {
                randKBAbility.Enabled = true;
                randKBAttacks.Enabled = true;
                randKBHitboxes.Enabled = true;
                if (!randEnemies.Checked && !randElements.Checked)
                {
                    randomize.Enabled = true;
                }
            }
            if (!randKB.Checked)
            {
                randKBAbility.Enabled = false;
                randKBAttacks.Enabled = false;
                randKBHitboxes.Enabled = false;
                if (!randEnemies.Checked && !randElements.Checked)
                {
                    randomize.Enabled = false;
                }
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            //Drag & Drop ROM files reading goes here soon(TM)
        }
    }
}
