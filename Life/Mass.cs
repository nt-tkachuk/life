using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace Life
{
    class Mass
    {
        protected MyPoint[,] Mas = new MyPoint[Constants.xLine, Constants.yLine];

        bool[,] masRepeate = new bool[Constants.xLine, Constants.yLine];
        bool[,] masRepeate0 = new bool[Constants.xLine, Constants.yLine];

        public Mass()
        {
            for (int i = 0; i < Constants.xLine; i++)
                for (int j = 0; j < Constants.yLine; j++)
                    Mas[i, j] = new MyPoint();
        }

        public bool pointData(int stepX, int StepY)
        {
            Random rd = new Random();

            int z = 0, q = 0;

            for (int i = 0; i < Constants.xLine; i++)
            {
                for (int j = 0; j < Constants.yLine; j++)
                {
                    if (rd.Next(2) == 0)
                        Mas[i, j].Live = false;
                    else
                        Mas[i, j].Live = true;
                    Mas[i, j].X = z;
                    Mas[i, j].Y = q;
                    q += StepY;
                }
                z += stepX;
                q = 0;
            }
            return true;
        }

        public MyPoint[,] returnMas()
        {
            return Mas;
        }

        public int returnPointX(int i, int j)
        {
            return Mas[i, j].X;
        }

        public int returnPointY(int i, int j)
        {
            return Mas[i, j].Y;
        }

        public void editLive(int i, int j)
        {
            if (i >= 0 && i <= Constants.xLine)
                if (Mas[i, j].Live == true)
                    Mas[i, j].Live = false;
                else
                    Mas[i, j].Live = true;
        }

        //Жива точка?
        public bool alive(int i, int j)
        {
            if (Mas[i, j].Live == true)
                return true;
            return false;
        }

        //Есть хоть кто-то живой?
        public bool livingElement()
        {
            bool k = false;
            for (int i = 0; i < Constants.xLine; i++)
                for (int j = 0; j < Constants.yLine; j++)
                    if (Mas[i, j].Live == true)
                    {
                        k = true;
                        return true;
                    }
            return false;
        }


        /*!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!*/
        public void processOfStep()
        {
            Array.Copy(masRepeate, masRepeate0, masRepeate.Length);
            masRepeate = dataLiveInBool(masRepeate);//тут же мас еще старый


            bool[,] masAdd = new bool[Constants.xLine, Constants.yLine];
            bool[,] masDel = new bool[Constants.xLine, Constants.yLine];
            //////////////////////
            masAdd = dataLiveInBool(masAdd);
            Array.Copy(masAdd, masDel, masAdd.Length);

            masAdd = addLiveElem(masAdd);
            masAdd = deliteLiveElem(masDel, masAdd);

            this.Mas = dataLiveInMyPoint(masAdd);
        }

        private bool[,] dataLiveInBool(bool[,] mass)
        {
            for (int i = 0; i < Constants.xLine; i++)
                for (int j = 0; j < Constants.yLine; j++)
                    if (Mas[i, j].Live == true)
                        mass[i, j] = true;
                    else mass[i, j] = false;
            return mass;
        }

        private MyPoint[,] dataLiveInMyPoint(bool[,] mass)
        {
            for (int i = 0; i < Constants.xLine; i++)
                for (int j = 0; j < Constants.yLine; j++)
                    if (mass[i, j] == true)
                        Mas[i, j].Live = true;
                    else Mas[i, j].Live = false;
            return Mas;
        }

        private int countLiveElem(bool[,] newMas, int ii, int jj)
        {
            int k = 0;

            for (int i = ii - 1; i <= ii + 1; i++)
                if ( i < Constants.xLine && i >= 0)
                for (int j = jj - 1; j <= jj + 1; j++)
                    if (j >= 0 && j < Constants.yLine)
                        if (i == ii && j == jj)
                        { }
                        else
                            if (newMas[i, j] == true)
                                k++;
                return k;
        }

        private bool[,] addLiveElem(bool[,] newMas)
        {
            bool[,] arr1 = (bool[,])newMas.Clone();

            for (int i = 0; i < Constants.xLine; i++)
                for (int j = 0; j < Constants.yLine; j++)
                    if (newMas[i, j] == false)
                        if (countLiveElem(newMas, i, j) == 3)
                            arr1[i, j] = true;
            return arr1;
        }

        private bool[,] deliteLiveElem(bool[,] masDel, bool[,] masAdd)
        {
            int k = 0;
            for (int i = 0; i < Constants.xLine; i++)
                for (int j = 0; j < Constants.yLine; j++)
                    if (masDel[i, j] == true)
                    {
                        k = countLiveElem(masDel, i, j);
                        if (k > 3 || k < 2)
                            masAdd[i, j] = false;
                    }
            return masAdd;
        }

        public bool repeat()
        {
            for (int i = 0; i < Constants.xLine; i++)
                for (int j = 0; j < Constants.yLine; j++ )
                    if (Mas[i,j].Live != masRepeate[i,j])
                        return true;
            return false;
        }

        public bool repeat0()
        {
            for (int i = 0; i < Constants.xLine; i++)
                for (int j = 0; j < Constants.yLine; j++)
                    if (Mas[i, j].Live != masRepeate0[i, j])
                        return true;
            return false;
        }

        public void ClearMas()
        {
            for(int i=0; i<Constants.xLine; i++)
                for(int j=0; j<Constants.yLine; j++)
                {
                    this.Mas[i, j].Live = false;
                    this.masRepeate[i, j] = false;
                    this.masRepeate0[i, j] = false;
                }
        }
    }
}

