using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace Bastyon
{
    public static class BuildingCompController
    {

    }

    public class PlaceWorker_RepellerDesignator : PlaceWorker
    {
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
        {
            // On selection of buiding in architect menue - show ring around the item
            range = def.specialDisplayRadius;
            GenDraw.DrawRadiusRing(center, this.range);

            //find nearest second building and draw a line between them.
            List<Thing> forCell = Find.CurrentMap.listerBuldingOfDefInProximity.GetForCell(center, this.range, def);
            List<Dictionary<string, object>> ConnectorPairs = new List<Dictionary<string, object>>();

            foreach (Thing node in forCell)
            {
                ConnectorPairs.Add(new Dictionary<string, object> {
                    {"CurrentNode", node.Position },
                    {"xVector", forCell.OrderBy(xValue => Math.Abs(node.Position.x - xValue.Position.x)).First().Position},
                    { "zVector", forCell.OrderBy(zValue => Math.Abs(node.Position.z - zValue.Position.z)).First().Position},
                    { "xConnection", false},
                    { "zConnection", true}
                });
            }

            if (forCell.Count > 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    Dictionary<string, object> NodeConnectors = ConnectorPairs[i];
                    bool xConnector = Convert.ToBoolean(NodeConnectors["xConnection"]);
                    bool zConnector = Convert.ToBoolean(NodeConnectors["zConnection"]);
                    var xNode = NodeConnectors["xVector"];
                    var zNode = NodeConnectors["zVector"];
                    var currNode = NodeConnectors["CurrentNode"];

                    foreach (Dictionary<string, object> nodeConnector in ConnectorPairs)
                    {
                        if (xConnector == false)
                        {
                            GenDraw.DrawLineBetween(GenThing.TrueCenter(center, Rot4.North, def.size, def.Altitude), (Vector3)xNode, SimpleColor.Green, 0.2f);
                            ConnectorPairs[i]["xConnection"] = true;
                            foreach (Dictionary<String, object> x in ConnectorPairs)
                            {
                                if(x["CurrentNode"] == ConnectorPairs[i]["xConnection"])
                                {
                                    x["xConnection"] = true;
                                    break;
                                }
                            }
                        }
                        if (zConnector == false)
                        {
                            GenDraw.DrawLineBetween(GenThing.TrueCenter(center, Rot4.North, def.size, def.Altitude), (Vector3)zNode, SimpleColor.Green, 0.2f);
                            ConnectorPairs[i]["zConnection"] = true;
                            foreach (Dictionary<String, object> z in ConnectorPairs)
                            {
                                if (z["CurrentNode"] == ConnectorPairs[i]["zConnection"])
                                {
                                    z["zConnection"] = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }


        public float range;
    }
    
    public class LinkBuildingNodeController
    {
        
        

    }
}
