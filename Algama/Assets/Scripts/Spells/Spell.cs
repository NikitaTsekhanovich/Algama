using UnityEngine;

namespace Spells
{
    public interface ISpell
    {
        
    }
    
    public abstract class Spell : MonoBehaviour, ISpell
    {
        protected abstract float Lifetime { get; set; }
        
        private float _evaluateTime;

        // Start is called before the first frame update
        protected void Start()
        {
            _evaluateTime = 0;
        }

        // Update is called once per frame
        protected void Update()
        {
            if (_evaluateTime >= Lifetime)
            {
                Destroy(gameObject);
            }
                

            _evaluateTime += Time.deltaTime;
        }
    }
}
