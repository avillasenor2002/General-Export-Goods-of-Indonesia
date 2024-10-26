using System.Collections.Generic;
using UnityEngine;
using static EnemyScript;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemies/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public List<EnemyForm> forms = new List<EnemyForm>();
}


