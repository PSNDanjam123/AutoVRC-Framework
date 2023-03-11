
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace AutoVRC.Framework.Helpers
{

    public class Scene
    {
        public static T[] GetAllChildrensComponent<T>(GameObject gameObject)
        {
            var childGameObjects = GetAllChildrenWithComponent<T>(gameObject);
            var count = childGameObjects.Length;
            var data = new T[count];
            for (var i = 0; i < count; i++)
            {
                data[i] = childGameObjects[i].GetComponent<T>();
            }
            return data;
        }

        [RecursiveMethod]
        public static GameObject[] GetAllChildrenWithComponent<T>(GameObject gameObject)
        {
            // Init array with total component count
            var count = CountAllChildrenWithComponent<T>(gameObject);
            var data = new GameObject[count];

            var index = 0;

            // If object has the component then add to array
            if (gameObject.GetComponent<T>() != null)
            {
                data[index] = gameObject;
                index++;
            }

            var transform = gameObject.transform;
            var childCount = transform.childCount;

            // if the object doesnt have any children then return the result
            if (childCount == 0)
            {
                return data;
            }

            // if it does have children then loop through and retrieve data
            for (var i = 0; i < childCount; i++)
            {
                var childData = GetAllChildrenWithComponent<T>(transform.GetChild(i).gameObject);
                for (var j = 0; j < childData.Length; j++)
                {
                    data[index] = childData[j];
                    index++;
                }
            }

            return data;
        }

        [RecursiveMethod]
        public static uint CountAllChildrenWithComponent<T>(GameObject gameObject)
        {
            uint count = 0;

            // if object has component increment the count
            if (gameObject.GetComponent<T>() != null)
            {
                count++;
            }

            var transform = gameObject.transform;
            var childCount = transform.childCount;

            // if the object doesnt have any children then return the result
            if (childCount == 0)
            {
                return count;
            }

            // if it does then add the childrens result to the count
            for (var i = 0; i < childCount; i++)
            {
                count += CountAllChildrenWithComponent<T>(transform.GetChild(i).gameObject);
            }

            return count;
        }
    }

}