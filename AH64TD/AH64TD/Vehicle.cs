using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AH64TD
{

    class Vehicle
    {

        public int HealthValue;
        public int MaxHealth;
        public String VehicleName;
        public VEHICLE_TYPE VehicleType;
        public Vector2 Position;
        public Texture2D txVehicleTexture;
        public Texture2D txRotorTexture;
        public float Velocity = (float)0.2; // Can override for different vehicles, of course.

        public Vehicle()
        {
        }

        public Vehicle(String strVehicleName, VEHICLE_TYPE type, Vector2 vPosition, Texture2D txVehicleTexture, Texture2D txRotorTexture)
        {

            VehicleName = strVehicleName;
            VehicleType = type;
            Position = vPosition;
            this.txVehicleTexture = txVehicleTexture;
            this.txRotorTexture = txRotorTexture;
            this.HealthValue = 100;
            this.MaxHealth = 100;

        }

    }

    enum VEHICLE_TYPE
    {
        PLAYER,
        ALLY,
        ENEMY,
        ENVIRONMENT
    };
    
}