using UnityEngine;

namespace src
{
    public class Main : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void Update()
        {
            GetInnput();
            
            Fractal.rootTransform.Rotate(60*Time.deltaTime, 30*Time.deltaTime, 60*Time.deltaTime);
        }

        private void GetInnput()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Fractal.Decrease();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Fractal.Increase();
            }
        }
    }
}
