using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SAE
{
    public class MenuController : MonoBehaviour
    {
        public bool level1Accessible;
        public bool controlsAccessible;
        public bool creditsAccessible;
        public bool mainMenuAccessible;

  
        void Update()
        {
            //GameplayLevel
            if ( SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 1) == true )
            {
                if( level1Accessible == true ) 
                { 
                    SceneManager.LoadScene("Level1");  
                }
                
            }
            //Controls
            if ( SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 2) == true )
            {
                if ( controlsAccessible == true )
                {
                    SceneManager.LoadScene("Controls");
                }       
            }
            //Credits
            if ( SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 3) == true )
            {
                if ( creditsAccessible == true )
                {
                    SceneManager.LoadScene("Credits");
                }   
            }
            //Main Menu
            if ( SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 4) == true )
            {
                if ( mainMenuAccessible == true )
                {
                    SceneManager.LoadScene("Main Menu");
                }      
            }
        }
    }
}
