// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace AzureIotEdgeSimulatedTemperatureSensor
{
    public class DataGenerationPolicy
    {
        private static readonly Random rnd = new Random();
        private double _normal;

        public DataGenerationPolicy()
        {
            MachineTemperatureMin = 60;
            MachineTemperatureMax = 110;
            MachinePressureMin = 1;
            MachinePressureMax = 10;
            AmbientTemperature = 21;
            HumidityPercentMin = 24;
            HumidityPercentMax = 27;
            _normal = (MachinePressureMax - MachinePressureMin) / (MachineTemperatureMax - MachineTemperatureMin);
            RunTimeSinceProductionMin = 5000;
            FirstMaintenance = 3000;
            FirstOverhaul = 4000;
        }

        public double MachineTemperatureMin { get; private set; }
        public double MachineTemperatureMax { get; private set; }
        public double MachinePressureMin { get; private set; }
        public double MachinePressureMax { get; private set; }
        public double AmbientTemperature { get; private set; }
        public int HumidityPercentMin { get; private set; }
        public int HumidityPercentMax { get; set; }
        public int RunTimeSinceProductionMin { get; private set; }
        public int FirstMaintenance {get; private set; }
        public int FirstOverhaul {get; private set;}

        public double CalculateMachineTemperature(double? currentTemperature = null)
        {
            var current = currentTemperature ?? MachineTemperatureMin;
            if(current > MachineTemperatureMax)
            {
                current += rnd.NextDouble() - 0.5; // add value between [-0.5..0.5]
            }
            else
            {
                current += -0.25 + (rnd.NextDouble() * 1.5); // add value between [-0.25..1.25] - avg +0.5
            }
            return current;
        }

        public double CalculatePressure(double currentTemperature)
        {
            return MachinePressureMin + ((currentTemperature - MachineTemperatureMin) * _normal);
        }

        public double CalculateAmbientTemperature()
        {
            return AmbientTemperature + rnd.NextDouble() -0.5;
        }

        public int CalculateHumidity()
        {
            return rnd.Next(HumidityPercentMin, HumidityPercentMax);
        }
        
        public int CalculateRunTimeSinceProduction(int? currentRunTime)
        {
            var current = currentRunTime ?? RunTimeSinceProductionMin;
            current += rnd.Next(3,15); //add value between [3..15]
            return current ;
        }
        public int CalculateRunTimeSinceMaintenance(int? currentRunTimeFromLastMaintenance)
        {
            var current = currentRunTimeFromLastMaintenance ?? FirstMaintenance;
            if (current - FirstMaintenance > 3000) 
            {
                current += rnd.Next(3,15); //add value between [3..15]
            }
            else
            {
                currentRunTimeFromLastMaintenance = 0;
                current = rnd.Next(3,15);
            }
            return current;
        }

        public int CalculateRunTimeSinceOverhaul(int? currenctRunTimeFromLastOverhaul)
        {
            var current = currenctRunTimeFromLastOverhaul ?? FirstOverhaul;
            return current += rnd.Next(3,15);
        }
    }
}
