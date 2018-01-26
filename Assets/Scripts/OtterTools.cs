using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All tools we need !
/// </summary>
public static class OtterTools
{
    /// <summary>
    /// Know if an animator as the specified parameter.
    /// </summary>
    /// <param name="paramName">Name of the parameter.</param>
    /// <param name="animator">Animator.</param>
    /// <returns></returns>
    public static bool HasParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Sort a list of transforms by distance between each transform and the <paramref name="target"/>.
    /// </summary>
    /// <param name="list">List of transforms that will be sorted.</param>
    /// <param name="target">Target.</param>
    public static void SortListByDistance(List<Transform> list, Transform target)
    {
        list.Sort((unit1, unit2) => (target.position - unit1.position).sqrMagnitude.CompareTo((target.position - unit2.position).sqrMagnitude));
    }
}