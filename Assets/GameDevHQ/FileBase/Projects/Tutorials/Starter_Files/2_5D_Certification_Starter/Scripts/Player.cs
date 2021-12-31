using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;

    private float _speed = 10.0f;

    private int _gravity = 1;

    private float _jumpHeight = 38.5f;

    private Vector3 _direction;

    private Vector3 _velocity;

    private Animator _anim;

    private bool _jumping = false;

    private bool _onLedge;

    private Player_Ledge_Checker _activeLedge;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
       PlayerMovement();

        if(_onLedge == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _anim.SetTrigger("Climb");
            }
        }
    }


    void PlayerMovement()
    {
        if (_characterController.isGrounded == true)
        {

            if (_jumping == true)
            {
                _jumping = false;
                _anim.SetBool("Jump", _jumping);
            }

            float _horizontalInput = Input.GetAxisRaw("Horizontal");
            _anim.SetFloat("Speed", Mathf.Abs(_horizontalInput));
            _direction = new Vector3(0, 0, _horizontalInput);
            _velocity = _direction * _speed;


            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y += _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jump", _jumping);
            }

            if (_horizontalInput != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

        }

        _velocity.y -= _gravity;
        _characterController.Move(_velocity * Time.deltaTime);

    }

    public void GrabLedge(Vector3 handPos, Player_Ledge_Checker currentLedge)
    {
        _onLedge = true;

        _characterController.enabled = false;
        _anim.SetBool("Hang", true);
        transform.position = handPos;
        _anim.SetBool("Jump", false);
        _anim.SetFloat("Speed", 0.0f);
        _activeLedge = currentLedge;

    }

    public void ClimbUpComplete()
    {
        transform.position = _activeLedge.GetStandPos();
        _anim.SetBool("Hang", false);
        _characterController.enabled = true;
    }
}
