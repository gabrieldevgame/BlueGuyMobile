using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    private struct PlayerInputConstants{
        public const string Horizontal = "Horizontal";
        public const string Jump = "Jump";
        public const string Attack = "Attack";
    }

    //MOVIMENTAÇÃO TECLADO E MOBILE
    public Vector2 GetMovementInput(){
        //Input teclado
        float horizontalInput = Input.GetAxisRaw(PlayerInputConstants.Horizontal);
        
        //Input mobile
        //Se o input do teclado for zero, tentamos ler o input do celular
        if(Mathf.Approximately(horizontalInput, 0.0f)){
            horizontalInput = CrossPlatformInputManager.GetAxisRaw(PlayerInputConstants.Horizontal);
        }

        return new Vector2(horizontalInput, 0);
    }

    //PULO TECLADO E PC
    //Pulo total ao apertar e soltar
    public bool IsJumpButtonDown(){
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.Space);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Jump);
        return isKeyboardButtonDown || isMobileButtonDown;
    }

    public bool IsAttackButtonDown(){
        bool isKeyboardButtonDown = Input.GetKeyDown(KeyCode.Z);
        bool isMobileButtonDown = CrossPlatformInputManager.GetButtonDown(PlayerInputConstants.Attack);
        return isKeyboardButtonDown || isMobileButtonDown;
    }
}
