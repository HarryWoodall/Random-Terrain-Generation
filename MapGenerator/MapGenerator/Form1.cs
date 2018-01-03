using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapGenerator {
    public partial class Form1 : Form {

        Panel[,] map;
        int pixelSize = 20;
        int mapX;
        int mapY;
        Random random = new Random();

        public Form1() {
            InitializeComponent();
            mapX = mainContainer.Width / pixelSize;
            mapY = mainContainer.Height / pixelSize;
            map = new Panel[mapX, mapY];
            createMap();
            Color color = populateMap(populateMap(populateMap(Color.Green)));
            addNoise();
        }

        private void createMap() {
            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {
                    Panel panel = new Panel();
                    panel.Padding = new Padding(0);
                    panel.Margin = new Padding(0);
                    panel.Size = new Size(pixelSize, pixelSize);
                    panel.BorderStyle = BorderStyle.FixedSingle;
                    panel.BackColor = Color.White;
                    mainContainer.Controls.Add(panel);
                    map[j, i] = panel;
                    if (random.Next(1, 30) > 28) {
                        map[j, i].BackColor = Color.Green;
                    }
                }
            }
        }

        private Color populateMap(Color color) {
            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {
                    if (map[j,i].BackColor == color) {

                        if (j !=0) {
                            map[j - 1, i].BackColor = Color.FromArgb(color.A - 30, color);
                        }
                        if (j != mapX - 1) {
                            map[j + 1, i].BackColor = Color.FromArgb(color.A - 30, color);
                        }

                        if (i != 0) {
                            map[j, i - 1].BackColor = Color.FromArgb(color.A - 30, color);
                        }
                        if (i != mapY - 1) {
                            map[j, i + 1].BackColor = Color.FromArgb(color.A - 30, color);
                        }
                    }
                }
            }
            return Color.FromArgb(color.A - 30, color);
        }

        private void addNoise() {
            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {
                    int counter = 0;
                    
                    if (i != mapY - 1 && map[j, i + 1].BackColor != Color.White) {
                        counter++;
                    }

                    if (i != 0 && map[j, i - 1].BackColor != Color.White) {
                        counter++;
                    }

                    if (j != mapX - 1 &&map[j + 1, i].BackColor != Color.White) {
                        counter++;
                    }

                    if (j != 0 && map[j - 1, i].BackColor != Color.White) {
                        counter++;
                    }

                    if (counter >= 3) {
                        map[j, i].BackColor = Color.Green;
                    }
                }
            }
        }
    }
}
