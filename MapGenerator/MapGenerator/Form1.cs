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

        byte[,] map;
        int pixelSize = 5;
        int mapX;
        int mapY;
        int startX;
        int startY;
        short seedValue;

        Random random = new Random();
        Graphics g;
        SolidBrush b;

        float centerValue;

        public Form1() {
            InitializeComponent();
            mapX = 900 / pixelSize;
            mapY = 500 / pixelSize;
            startX = 92;
            startY = 44;
            map = new byte[mapX, mapY];
            centerValue = 0;
            seedValue = 100;
        }

        public void redrawMap() {
            resetMap();
            addSeed();
            addTexture01();
            addTexture02();
            increaseNoise();
            increaseNoise02();
            addFilling();
            colorCorrection();
        }

        public void resetMap() {
            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {
                    map[j, i] = 0;
                }
            }
        }

        public void addSeed() {
            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {
                    float ratio1 = (3 * (1 - centerValue)) + 1;
                    float ratio2 = centerValue * 50;

                    float landValue = (((float)Math.Min(j, mapX - j) * (float)Math.Min(i, mapY - i)) / 2) / (((float)mapY * (float)mapX) / 2);

                    if (random.Next(0, seedValue) > Convert.ToInt32(seedValue - ((ratio2 * landValue) + ratio1))) {
                        map[j, i] = 1;
                    }
                }
            }
        }

        public void addTexture01() {
            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {

                    if (map[j,i] == 1) {

                        if (j != 0 && i != 0) {
                            map[j - 1, i - 1] = 2;
                        }

                        if (j != 0) {
                            map[j - 1, i] = 2;
                        }

                        if (j != 0 && i != mapY - 1) {
                            map[j - 1, i + 1] = 2;
                        }

                        if (i != 0) {
                            map[j, i - 1] = 2;
                        }

                        if (i != mapY - 1) {
                            map[j, i + 1] = 2;
                        }

                        if (j != mapX - 1 && i != 0) {
                            map[j + 1, i - 1] = 2;
                        }

                        if (j != mapX - 1) {
                            map[j + 1, i] = 2;
                        }

                        if (j != mapX - 1 && i != mapY - 1) {
                            map[j + 1, i + 1] = 2;
                        }
                    }
                }
            }
        }

        public void addTexture02() {
            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {

                    if (map[j, i] == 2) {

                        if (j != 0) {
                            if (!(map[j - 1, i] == 2 || map[j - 1, i] == 1)) {
                                map[j - 1, i] = 3;
                            }
                        }

                        if (i != 0) {
                            if (!(map[j, i - 1] == 2 || map[j, i - 1] == 1)) {
                                map[j, i - 1] = 3;
                            }
                        }

                        if (i != mapY - 1) {
                            if (!(map[j, i + 1] == 2 || map[j, i + 1] == 1)) {
                                map[j, i + 1] = 3;
                            }
                        }

                        if (j != mapX - 1) {
                            if (!(map[j + 1, i] == 2 || map[j + 1, i] == 1)) {
                                map[j + 1, i] = 3;
                            }
                        }
                    }
                }
            }
        }

        public void addFilling() {
            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {

                    if (map[j, i] == 0) {
                        byte counter = 0;

                        if (j != 0) {
                            if (map[j - 1, i] != 0) {
                                counter++;
                            }
                        }

                        if (i != 0) {
                            if (map[j, i - 1] != 0) {
                                counter++;
                            }
                        }

                        if (i != mapY - 1) {
                            if (map[j, i + 1] != 0) {
                                counter++;
                            }
                        }

                        if (j != mapX - 1) {
                            if (map[j + 1, i] != 0) {
                                counter++;
                            }
                        }

                        if (counter >= 3) {
                            map[j, i] = 4;
                        }
                    }
                }
            }
        }

        public void increaseNoise() {
            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {

                    if (map[j,i] == 3) {

                        if (random.Next(0,6) >= 1) {

                            if (j != 0) {
                                if (map[j - 1, i] == 0) {
                                    map[j - 1, i] = 4;
                                    continue;
                                }
                            }

                            if (i != 0) {
                                if (map[j, i - 1] == 0) {
                                    map[j, i - 1] = 4;
                                    continue;
                                }
                            }

                            if (i != mapY - 1) {
                                if (map[j, i + 1] == 0) {
                                    map[j, i + 1] = 4;
                                    continue;
                                }
                            }

                            if (j != mapX - 1) {
                                if (map[j + 1, i] == 0) {
                                    map[j + 1, i] = 4;
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void increaseNoise02() {
            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {

                    if (map[j, i] == 4) {

                        if (random.Next(0, 4) >= 1) {

                            if (j != 0) {
                                if (map[j - 1, i] == 0) {
                                    map[j - 1, i] = 4;
                                    continue;
                                }
                            }

                            if (i != 0) {
                                if (map[j, i - 1] == 0) {
                                    map[j, i - 1] = 4;
                                    continue;
                                }
                            }

                            if (i != mapY - 1) {
                                if (map[j, i + 1] == 0) {
                                    map[j, i + 1] = 4;
                                    continue;
                                }
                            }

                            if (j != mapX - 1) {
                                if (map[j + 1, i] == 0) {
                                    map[j + 1, i] = 4;
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void colorCorrection() {
            ccRound01();
            ccRound02();
        }

        public void ccRound01() {
            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {

                    if (map[j, i] != 0) {

                        if (j != 0) {
                            if (map[j - 1, i] == 0) {
                                map[j, i] = 5;
                                continue;
                            }
                        }

                        if (i != 0) {
                            if (map[j, i - 1] == 0) {
                                map[j, i] = 5;
                                continue;
                            }
                        }

                        if (i != mapY - 1) {
                            if (map[j, i + 1] == 0) {
                                map[j, i] = 5;
                                continue;
                            }
                        }

                        if (j != mapX - 1) {
                            if (map[j + 1, i] == 0) {
                                map[j, i] = 5;
                                continue;
                            }
                        }
                    }
                }
            }
        }

        public void ccRound02() {
            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {

                    if (!(map[j, i] == 0 || map[j, i] == 5)) {

                        if (j != 0) {
                            if (map[j - 1, i] == 5) {
                                map[j, i] = 6;
                                continue;
                            }
                        }

                        if (i != 0) {
                            if (map[j, i - 1] == 5) {
                                map[j, i] = 6;
                                continue;
                            }
                        }

                        if (i != mapY - 1) {
                            if (map[j, i + 1] == 5) {
                                map[j, i] = 6;
                                continue;
                            }
                        }

                        if (j != mapX - 1) {
                            if (map[j + 1, i] == 5) {
                                map[j, i] = 6;
                                continue;
                            }
                        }
                    }
                }
            }
        }

        public void drawRectangle(int x, int y, Color color) {
            b = new SolidBrush(color);
            g = this.CreateGraphics();
            g.FillRectangle(b, new Rectangle( x, y, pixelSize, pixelSize));
            b.Dispose();
            g.Dispose();
        }

        private void genButton_Click(object sender, EventArgs e) {

            paintOver();
            redrawMap();

            for (int i = 0; i < mapY; i++) {
                for (int j = 0; j < mapX; j++) {
                    //drawHeatMap(i, j);
                    drawLandMap(i, j);
                }
            }
        }

        private void paintOver() {
            b = new SolidBrush(Color.White);
            g = this.CreateGraphics();
            g.FillRectangle(b, new Rectangle(startX, startY, 900, 500));
            b.Dispose();
            g.Dispose();
        }

        private void drawLandMap(int i, int j) {
            if (map[j, i] == 0)
                drawRectangle(startX + (j * pixelSize), startY + (i * pixelSize), Color.FromArgb(255, 96, 204, 214));
            else if (map[j, i] == 5)
                drawRectangle(startX + (j * pixelSize), startY + (i * pixelSize), Color.FromArgb(255, 241, 241, 147));
            else if (map[j, i] == 6)
                drawRectangle(startX + (j * pixelSize), startY + (i * pixelSize), Color.LightGreen);
            else
                drawRectangle(startX + (j * pixelSize), startY + (i * pixelSize), Color.Green);
        }

        private void drawHeatMap(int i, int j) {
            if (map[j, i] == 1)
                drawRectangle(startX + (j * pixelSize), startY + (i * pixelSize), Color.Gray);
            if (map[j, i] == 2)
                drawRectangle(startX + (j * pixelSize), startY + (i * pixelSize), Color.Brown);
            if (map[j, i] == 3)
                drawRectangle(startX + (j * pixelSize), startY + (i * pixelSize), Color.Green);
            if (map[j, i] == 4)
                drawRectangle(startX + (j * pixelSize), startY + (i * pixelSize), Color.Blue);
            if (map[j, i] == 5)
                drawRectangle(startX + (j * pixelSize), startY + (i * pixelSize), Color.Green);
            if(map[j, i] == 6)
                drawRectangle(startX + (j * pixelSize), startY + (i * pixelSize), Color.Green);
        }

        private void centreBar_Scroll(object sender, EventArgs e) {
            centerValue = (float)centreBar.Value / 100;
        }
    }
}
