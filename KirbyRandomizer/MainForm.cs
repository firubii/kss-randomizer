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
            readROM.Filter = "SNES SMC ROM Files|*.smc|SNES SFC ROM Files|*.sfc|All Files|*";
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
                if (!randEnemies.Checked)
                {
                    randomize.Enabled = true;
                }
            }
            if (!randElements.Checked)
            {
                randElementsEach.Enabled = false;
                randOneElement.Enabled = false;
                if (!randEnemies.Checked)
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
                if (!randElements.Checked)
                {
                    randomize.Enabled = true;
                }
            }
            if (!randEnemies.Checked)
            {
                includeMinorEnemies.Enabled = false;
                randBossAbilities.Enabled = false;
                randMiniBossAbilities.Enabled = false;
                if (!randElements.Checked)
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
    }
}
