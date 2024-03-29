﻿using System.Data.SqlClient;

namespace Business
{
    public interface IMotor
    {

        public void RentMotor(int motorId, DateTime ophaal, DateTime inlever, int prijs, int huurderId);
        public Motor GetMotor(int motorId);
        public void EditMotor(string merk, int bouwjaar, int prijs, bool huurbaar, int motorId);
    }
}