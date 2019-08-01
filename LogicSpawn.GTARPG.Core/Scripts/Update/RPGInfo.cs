using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GTA;
using GTA.Native;

namespace LogicSpawn.GTARPG.Core
{
    public class RPGInfo : UpdateScript
    {
        public static bool IsWideScreen = true;
        public static Ped NearestPed = null;
        public static LootItem NearbyLoot = null;
        public static bool KeyboardActive = false;
        public static Vehicle[] NearbyVehicles = new Vehicle[0];
        public static Ped[] NearbyPeds = new Ped[0];

        public override void Update()
        {
            Ped player = Game.Player.Character;
            NearestPed = World.GetNearbyPeds(player, 3).FirstOrDefault();
            NearbyLoot = PlayerMethods.GetNearbyLoot(2.5f).FirstOrDefault();
            IsWideScreen = Function.Call<bool>(Hash.GET_IS_WIDESCREEN);
            Wait(400);
        }

    }
    public class RPGInfoAlt : UpdateScript
    {
	    public override void Update()
        {
            var entities = GTA.World.GetAllEntities();
            
            var veh = new List<Vehicle>();
            var ped = new List<Ped>();

            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
			    if (Function.Call<bool>(Hash.DOES_ENTITY_EXIST, entity))
			    {
				    switch (Function.Call<int>(Hash.GET_ENTITY_TYPE, entity))
				    {
					    case 1:
                            ped.Add(new Ped(entity.Handle));
						    break;
					    case 2:
                            veh.Add(new Vehicle(entity.Handle));
						    break;
				    }
			    }
		    }

            RPGInfo.NearbyVehicles = veh.ToArray();
            RPGInfo.NearbyPeds = ped.ToArray();
            Wait(10000);
        }

    }
}