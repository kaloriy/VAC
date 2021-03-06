﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMath
{
    public class Q:Math_Field
    {
        #region Конструкторы

        public Q(List<string> first, List<string> second)///нет исключения - Знаменатель не может быть равен нулю 
        {
            try
            {
                Numerator = new Z(first);
                Denominator = new Z(second);
                RED_Q_Q();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Поля

        private Z Numerator;
        private Z Denominator;

        #endregion

        #region Свойства 

        bool INT_Q_B//тест идет вместе с isDown 
        {
            get
            {
                List<string> one = new List<string>();
                one.Add("1");
                if (Denominator.COM(new Z(one)) == 0)
                    return true;
                else
                    return false;
            }
        }

        public Z Num
        {
            get
            {
                return Numerator.Clone();
            }
        }

        public Z Den
        {
            get
            {
                return Denominator.Clone();
            }
        }

        public byte POZ_Q_D// есть тесты
        {
            get
            {
                 if (Numerator.POZ_Z_D != 0)
                 {
                    if (Numerator.POZ_Z_D == 2)
                        return 2;
                    else
                        return 1;
                 }
                 else
                    return 0;
            }
        }

        #endregion

        #region Перегрузки 


        public static Q operator -(Q value) // MUL_QM_Q //есть тесты
        {
            Q Clone = value.Clone();
            Clone.Numerator = -Clone.Numerator;
            return Clone; 
        }

        public static Q operator +(Q first, Q second) // ADD_QQ_Q //есть тесты
        {
            Q Sum = first.Clone();
            Z ComDen = first.Denominator.Clone();
            if (first.Denominator.COM(second.Denominator) == 0)
            {
                Sum.Numerator = first.Numerator + second.Numerator;
                Sum.RED_Q_Q();
                return Sum;
            }
            else
            {
                ComDen = (Z)first.Denominator.LCM(second.Denominator);
                if (first.Denominator.COM(ComDen) == 0)
                {
                    Sum.Denominator = first.Denominator;
                    second.Numerator *= (first.Denominator / second.Denominator);
                    Sum.Numerator = first.Numerator + second.Numerator;
                }
                if (second.Denominator.COM(ComDen) == 0)
                {
                    Sum.Denominator = second.Denominator;
                    first.Numerator *= (second.Denominator / first.Denominator);
                    Sum.Numerator = first.Numerator + second.Numerator;
                }
                if (first.Denominator.COM(ComDen) != 0 && second.Denominator.COM(ComDen) != 0)
                {
                    Sum.Denominator = ComDen;
                    first.Numerator *= (ComDen / first.Denominator);
                    second.Numerator *= (ComDen / second.Denominator);
                    Sum.Numerator = first.Numerator + second.Numerator;
                }
                Sum.RED_Q_Q();
            }
            return Sum;
        }

        public static Q operator -(Q first, Q second) // SUB_QQ_Q //есть тесты
        {
            Q result;
            result = first + (-second);
            result.RED_Q_Q();
            return result;
        }

        public static Q operator *(Q first, Q second) // MUL_QQ_Q //есть тесты
        {
            Q result = first.Clone();
            result.Denominator *= second.Denominator;
            result.Numerator *= second.Numerator;
            result.RED_Q_Q();
            return result;
        }

        public static Q operator /(Q first, Q second) // DIV_QQ_Q // есть тесты
        {
            Q divider = null;
            Q result = first.Clone();
            divider.Denominator = second.Numerator;
            divider.Numerator = second.Denominator;
            if (divider.Numerator.isDown != second.Numerator.isDown)
            {
                divider = - divider;
            }
            result *= divider;
            result.RED_Q_Q();
            return result;
        }

        public static Z operator %(Q first, Q second)//есть тесты
        {
            List<string> nol = new List<string>();
            nol.Add("0");
            Z pog = new Z(nol);
            return pog;
        }

        public static Q operator ^(Q first, Z second)
        {
            Q num = first.Clone();
            Q nul1 = null;
            if (second.POZ_Z_D != 0)
            {
                for (int i = 0; i < Convert.ToInt32(second); i++)
                {
                    num.Numerator *= second;
                    num.Denominator *= second;
                }
                if (second.POZ_Z_D == 2)
                {
                    num.RED_Q_Q();
                    return num;
                }
                else
                {
                    nul1.Denominator = num.Numerator;
                    num.Numerator = num.Denominator;
                    num.Denominator = nul1.Denominator;

                    nul1.Numerator = num.Denominator;
                    num.Denominator = num.Numerator;
                    num.Numerator = nul1.Numerator;

                    num.RED_Q_Q();
                    return num;
                }
            }
            else
            {
                List<string> one = new List<string>();
                one.Add("1");
                return new Q(one, one);
            }
        }

        public static implicit operator List<string>(Q value)//есть тесты
        {
            List<string> one = new List<string>();
            one.Add("1");
            List<string> S = new List<string>();
            List<string> temp = new List<string>();
            temp = value.Numerator;
            S.AddRange(temp);
            if (value.Denominator.COM(new Z(one)) != 0)
            {
                S.Add("/");
                temp = value.Denominator;
                S.AddRange(temp);
            }

            return S;
        }

        public static explicit operator Z(Q value)//есть тесты
        {
            if (value.isDown)
            {
                return  value.Numerator.Clone();
            }
            else
            {
                return null;
            }
        }

        public static implicit operator Q(Z value)
        {
            List<string> s = new List<string>();
            s.Add("1");
            return new Q(value, s);
        }
                #endregion

        #region Методы 

        private void RED_Q_Q() //есть тесты
        {
            Z nod = (Z)(Numerator.GCF(Denominator));
            Numerator /= nod;
            Denominator /= nod;
        }
        
        public Q Clone() // Александр Баталин 9370//есть тесты
        {
            Q clone = new Q(Numerator.Clone(), Denominator.Clone());
            return clone;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == GetType() && this != null && obj != null)
            {
                Q sec = obj as Q;
                if (Denominator.Equals(sec.Denominator) && Numerator.Equals(sec.Numerator)) return true;
            }
            return false;
        }

        #endregion

        #region Общие свойства и методы

        #region Свойства

        public override int id
        {
            get
            {
                return 3;
            }
        }

        public override bool isDown//есть тесты
        {
            get
            {
                return INT_Q_B;
            }
        }

        public override Math_Field ABS
        {
            get
            {
                Q clone = Clone();
                clone.Numerator = clone.Numerator.ABS as Z;
                return clone;
            }
        }

        public override Math_Field DEG
        {
            get
            {
                List<string> s1 = new List<string>();
                s1.Add("0");
                List<string> s2 = new List<string>();
                s2.Add("1");
                return new Q(s1, s2);
            }
        }

        public override Math_Field UNT
        {
            get
            {
                return -this;
            }
        }

        public override Math_Field DER
        {
            get
            {
                List<string> s1 = new List<string>();
                s1.Add("0");
                List<string> s2 = new List<string>();
                s2.Add("1");
                return new Q(s1, s2);
            }
        }

        public override Math_Field FAC
        {
            get
            {
                return Clone();
            }
        }

        public override Math_Field LED
        {
            get
            {
                return Clone();
            }
        }

        #endregion

        #region Методы

        public override Math_Field MUL(Math_Field second)
        {
            return this * (second as Q);
        }

        public override Math_Field DIV(Math_Field second)
        {
            return this / (second as Q);
        }

        public override Math_Field ADD(Math_Field second)
        {
            return this + (second as Q);
        }

        public override Math_Field LCM(Math_Field second)
        {
            return null;
        }

        public override Math_Field Dawn()
        {
            return (Z)this;
        }

        public override Math_Field SUB(Math_Field second)
        {
            return this - (second as Q);
        }

        public override Math_Field MOD(Math_Field second)
        {
            return this % (second as Q);
        }

        public override byte COM(Math_Field second)
        {
            Q per2 = second as Q;
            Q per1 = Clone();
            Z nok;
            nok = (Z)per2.Denominator.LCM(per1.Denominator);
            per2.Numerator *= (nok / per2.Denominator);
            per1.Numerator *= (nok / per1.Denominator);
            per2.Denominator = nok;
            per1.Denominator = nok;
            return per1.Numerator.COM(per2.Numerator);
        }

        public override Math_Field GCF(Math_Field second)
        {
            return null;
        }

        public override List<string> ToListstring()
        {
            return this;
        }

        #endregion

        #endregion
    }
}
