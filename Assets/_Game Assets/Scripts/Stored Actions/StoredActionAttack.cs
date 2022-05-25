using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredActionAttack : StoredAction
{
    public StoredActionAttack(EntityCharacter entity, Vector3 direction)
    {
        // play animation, cek tile dir ada musuh apa engga
        bool firstCall = false;
        Transform transform = entity.transform;
        LevelManager levelManager = GameManager.Instance.levelManager;
        LevelGridNode currentNode = entity.currentNode;

        Vector3 localTarget = Vector3.right * direction.x + Vector3.forward * direction.z;

        action = () =>
        {
            if(!firstCall)
            {
                firstCall = true;
                entity.animator.SetTrigger("attack");
                entity.animator.SetInteger("direction", _ConvertDirToInt(direction));
            }

            actionHasDone = _CheckProcessInputHasOverMinimumTime();

            Vector3 nextPos = new Vector3(currentNode.realWorldPos.x + Mathf.RoundToInt(localTarget.x), transform.position.y, currentNode.realWorldPos.z + Mathf.RoundToInt(localTarget.z));
            LevelGrid tempGrid = levelManager.GetClosestGridFromPosition(nextPos);
            if (tempGrid == null) return;

            LevelGridNode tempGridNode = tempGrid.ConvertPosToNode(nextPos);
            if (tempGridNode == null) return;

            var monster = tempGridNode.CheckListEntityHaveComponent<EntityCharacterNPCMonsterFrog>();
            if(monster)
            {
                monster.Hit();
                CinemachineShake.Instance.ShakeCamera(5.0f, 0.1f); 
                actionHasDone = true;
                return;
            }

            var player = tempGridNode.CheckListEntityHaveComponent<EntityCharacterPlayer>();
            if(player)
            {
                player.Hit();
                CinemachineShake.Instance.ShakeCamera(5.0f, 1.0f);
                actionHasDone = true;
            }
        };
    }

    private int _ConvertDirToInt(Vector3 direction)
    {
        if (direction == Vector3.back) return 0;
        if (direction == Vector3.forward) return 1;
        if (direction == Vector3.left) return 2;
        if (direction == Vector3.right) return 3;

        return -1;
    }
}
