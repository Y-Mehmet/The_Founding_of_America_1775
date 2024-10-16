using BaseAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGeneralTextSo", menuName = "generalTextSo")]
public class GeneralTextSO : SingletonScriptableObject<MineIConSO>
{
    // Dictionary to store quotes based on battle outcomes
    public Dictionary<string, List<List<string>>> quotesDictionary = new Dictionary<string, List<List<string>>>
    {
        { "victory", new List<List<string>>
            {
                new List<string> { "The fight has only begun, and already we see the light of liberty shining brighter!",
                                   "A flicker of light is no match for the enduring power of the Crown." },
                new List<string> { "This victory is proof that the will of free men cannot be broken!",
                                   "A single victory will not save your rebellion from certain defeat." },
                new List<string> { "This triumph is yet another step towards the freedom we seek!",
                                   "Your steps are small, and your cause is weaker by the day." },
                new List<string> { "The determination of a people fighting for their freedom cannot be denied!",
                                   "Determined you may be, but no match for the power of the Empire." },
                new List<string> { "Another triumph for the cause of freedom! Our strength lies in the justice of our fight!",
                                   "Your fight is misguided, and the King’s justice will prevail in the end." },
                new List<string> { "Victory is the reward for those who fight for liberty, and today we claim it!",
                                   "Victory today, perhaps, but your uprising will be crushed in time." },
                new List<string> { "This victory shows the world that those who fight for their liberty cannot be defeated!",
                                   "Victory today does not mean victory tomorrow. The Empire's power endures." },
                new List<string> { "The courage of men fighting for their freedom is unmatched, as shown today!",
                                   "Your courage is no match for the discipline of the King’s army." },
                new List<string> { "This battle shows that our resolve for freedom cannot be extinguished!",
                                   "Your resolve will soon crumble under the weight of the Empire." },
                new List<string> { "This triumph today is a testament to the strength of our cause!",
                                   "Your cause is a mere whisper against the roar of the King's forces." }
            }
        },
        { "draw", new List<List<string>>
            {
                new List<string> { "Neither side claims victory today, but the flame of freedom still burns within us.",
                                   "Your flame will soon be extinguished under the weight of the Empire." },
                new List<string> { "We stand firm today, but the war is far from over.",
                                   "Standing firm means little when the might of the Empire is against you." },
                new List<string> { "This battle is drawn, but freedom’s fight is far from finished.",
                                   "Your fight will be finished soon enough by the forces of the King." },
                new List<string> { "No victor has emerged today, but our will to fight for liberty burns brighter.",
                                   "That flame will flicker out soon enough under the Crown’s relentless advance." },
                new List<string> { "The battle ends without a victor, but our resolve is unshakable.",
                                   "Unshakable? The Empire’s forces will soon prove otherwise." }
            }
        },
        { "defeat", new List<List<string>>
            {
                new List<string> { "We may lose the ground today, but our cause is unyielding!",
                                   "Your rebellion weakens with every defeat; the King’s forces remain ever steadfast." },
                new List<string> { "A setback today, but we live to fight another day, stronger in our resolve.",
                                   "You may retreat, but you cannot escape the reach of His Majesty’s justice." },
                new List<string> { "Though we retreat today, our cause is as alive as ever, and we will return.",
                                   "Your cause is doomed to fail, and your retreats will become routs." },
                new List<string> { "This defeat will only fuel the fires of our determination; we shall return stronger.",
                                   "Your fires are burning out, and soon they will be nothing but ashes." },
                new List<string> { "A loss today will not deter us. We fight on, and liberty will prevail!",
                                   "Hardships will soon bring your rebellion to its knees." }
            }
        }
    };
    public string a;
}
