using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectExample
{
    public class TimeLineManager : MonoBehaviour, IExposedPropertyTable
    {
        public PlayAnimationClip playAniClip;

        public List<PropertyName> listPropertyName;
        public List<UnityEngine.Object> listReference;

        public void ClearReferenceValue(PropertyName id)
        {
            int index = listPropertyName.IndexOf(id);
            if (index != -1)
            {
                listReference.RemoveAt(index);
                listPropertyName.RemoveAt(index);
            }
        }

        public Object GetReferenceValue(PropertyName id, out bool idValid)
        {
            int index = listPropertyName.IndexOf(id);
            if (index != -1)
            {
                idValid = true;
                return listReference[index];
            }
            idValid = false;
            return null;
        }
        public void SetReferenceValue(PropertyName id, Object value)
        {
            int index = listPropertyName.IndexOf(id);
            if (index != -1)
            {
                listReference[index] = value;
            }
            else
            {
                listPropertyName.Add(id);
                listReference.Add(value);
            }
        }
    }
}