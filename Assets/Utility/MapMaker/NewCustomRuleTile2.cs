using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class NewCustomRuleTile2 : RuleTile<NewCustomRuleTile.Neighbor>
{
    public bool customField;
    public class Neighbor : RuleTile.TilingRule.Neighbor
    {
        public const int Null = 3;
        public const int NotNull = 4;
    }

    public override bool RuleMatch(int neighbor, TileBase tile)
    {
        switch (neighbor)
        {
            case Neighbor.Null: return tile == null;
            case Neighbor.NotNull: return tile != null;
        }
        return base.RuleMatch(neighbor, tile);
    }
    public override bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, ref Matrix4x4 transform)
    {
        int angle2 = (int)m_DefaultGameObject.transform.rotation.eulerAngles.y;
            if (RuleMatches(rule, position, tilemap,angle2))
            {
                transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, angle2, 0f), Vector3.one);
            return true;
            }

        // Check rule against rotations of 0, 90, 180, 270
        if (rule.m_RuleTransform == TilingRuleOutput.Transform.Rotated)
        {
            for (int angle = m_RotationAngle + (int)m_DefaultGameObject.transform.rotation.eulerAngles.y; angle <= 360; angle += m_RotationAngle)
            {
                if (RuleMatches(rule, position, tilemap, angle))
                {
                    transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, angle, 0f), Vector3.one);
                    return true;
                }
            }
        }
        // Check rule against x-axis, y-axis mirror
        else if (rule.m_RuleTransform == TilingRuleOutput.Transform.MirrorXY)
        {
            if (RuleMatches(rule, position, tilemap, true, true))
            {
                transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(-1f, -1f, 1f));
                return true;
            }
            if (RuleMatches(rule, position, tilemap, true, false))
            {
                transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(-1f, 1f, 1f));
                return true;
            }
            if (RuleMatches(rule, position, tilemap, false, true))
            {
                transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1f, -1f, 1f));
                return true;
            }
        }
        // Check rule against x-axis mirror
        else if (rule.m_RuleTransform == TilingRuleOutput.Transform.MirrorX)
        {
            if (RuleMatches(rule, position, tilemap, true, false))
            {
                transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(-1f, 1f, 1f));
                return true;
            }
        }
        // Check rule against y-axis mirror
        else if (rule.m_RuleTransform == TilingRuleOutput.Transform.MirrorY)
        {
            if (RuleMatches(rule, position, tilemap, false, true))
            {
                transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1f, -1f, 1f));
                return true;
            }
        }

        return false;
    }
}