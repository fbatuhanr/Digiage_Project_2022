/*
using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey;
    
    [Header("Press '?' to Interact: ")]
    [SerializeField] private UIPanelAnimationController interactionStartUI;
    
    [Header("Conversation Canvases: ")]
    [SerializeField] private UIPanelAnimationController conversationAnimUI;
    [SerializeField] private ConversationUIController conversationUIController;

    [Header("Selected Object Canvases: ")] 
    private EnemyController _enemyController;
    private UIPanelAnimationController _enemyObjectAnimUI;
    public ConversationUIController enemyObjectUIController;

    private bool _isInteractStartLabelEnabled;
    private bool _isConversationEnabled;
    private PlayerConversationScriptableObject.PlayerConversation _playerConversationsData;
    private List<EnemyConversationScriptableObject.EnemyConversation> _enemyConversationsData;

    public string enemyTag;

    private int _currentConversationNumber;
    public int _conversationCounter;


    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float rayDistance=1f;
    private Vector3 RayOrigin => transform.position;
    private Vector3 RayDirection => transform.right;
    
    
    private PlayerMovement _playerMovement;
    private PlayerController _playerController;
    [SerializeField] private SelectionsUIController selectionsUIController;
    
    private void Start()
    {
        enemyTag = null;
        _enemyController = null;
        enemyObjectUIController = null;
        _enemyObjectAnimUI = null;
        
        _isInteractStartLabelEnabled = false;
        _isConversationEnabled = false;

        _enemyConversationsData = null;

        _currentConversationNumber = 0;
        _conversationCounter = 0;
        
        _playerMovement = GetComponent<PlayerMovement>();
        _playerController = GetComponent<PlayerController>();
    }

    private bool Raycast(out RaycastHit hitInfo)
    {
        return Physics.Raycast(RayOrigin, RayDirection, out hitInfo, rayDistance, interactionLayer);
    }
    private void CheckInteractionAvailable()
    {
        var isHit = Raycast(out var hitInfo); DebugDrawRay(isHit);
        if (isHit)
        {
            var hitCollider = hitInfo.collider;
            if (!_isInteractStartLabelEnabled)
            {
                interactionStartUI.Enable();

                enemyTag = hitCollider.tag;

                _enemyController = hitCollider.GetComponent<EnemyController>();
                enemyObjectUIController = hitCollider.GetComponentInChildren<ConversationUIController>();
                _enemyObjectAnimUI = hitCollider.GetComponentInChildren<UIPanelAnimationController>();
                _isInteractStartLabelEnabled = true;
            }
        }
        else
        {
            enemyObjectUIController = null;
            if (_isInteractStartLabelEnabled)
            {
                interactionStartUI.Disable();
                DisablePlayerConversation();
                DisableEnemyConversation();
                _isInteractStartLabelEnabled = false;
            }
        }
    }

    private void DebugDrawRay(bool anyHit)
    {
        Debug.DrawRay(RayOrigin, RayDirection * rayDistance, anyHit ? Color.green : Color.red);
    }
    
    private void Update()
    {
        /*CheckInteractionAvailable();
        
        if (Input.GetKeyDown(interactKey))
        {
            if (!_isConversationEnabled && _isInteractStartLabelEnabled)
            {
                InteractionStartedWithKeyPress();
            }
            else if (_isConversationEnabled)
            {
                ContinueInteractionWithKeyPress();
            }
        }#1#
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DialogTrigger dialogTrigger))
        {
            dialogTrigger.Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out DialogTrigger dialogTrigger))
        {
            dialogTrigger.Deactivate();
        }
    }

    private void InteractionStartedWithKeyPress()
    {
        SetPlayerAndEnemyConversation();
        PreparePlayerConversation();
                
        _isConversationEnabled = true;
    }

    private void ContinueInteractionWithKeyPress()
    {
        if (conversationAnimUI.status == Status.Enabled)
        {
            PrepareEnemyConversation();
        }
        else if (_enemyObjectAnimUI.status == Status.Enabled)
        {
            _conversationCounter++;

            if(_playerConversationsData.data[_conversationCounter] != null)
                PreparePlayerConversation();
            else
            {
                _isConversationEnabled = false;
                _isInteractStartLabelEnabled = true;
                DisableEnemyConversation();
                _playerMovement.UnlockPlayer();
                _currentConversationNumber++;
            }
        }
    }


    private void SetPlayerAndEnemyConversation()
    {
        
        var x = _playerController.playerConversationData.playerConversations.SingleOrDefault(x => x.targetTag == enemyTag);
        _playerConversationsData = x;
        
        _enemyConversationsData = _enemyController.enemyConversationData.enemyConversations;
        
        _playerMovement.LockPlayer();
        _conversationCounter = 0;
    }

    private void PreparePlayerConversation()
    {
        if (_conversationCounter > 0 && _enemyConversationsData[_conversationCounter-1].dataAnswers.Length > 0)
        {
            var firstData = _enemyConversationsData[_conversationCounter-1].dataAnswers[0];
            var secondData = _enemyConversationsData[_conversationCounter-1].dataAnswers[1];

            Debug.Log("first data: " + firstData);
            Debug.Log("second data: " + secondData);
                
            selectionsUIController.Enable(firstData, secondData);
        }
        else
        {
            SetPlayerConversationLabelText();
            EnablePlayerConversation();
        }
    }
    private void SetPlayerConversationLabelText()
    {
        var content = _playerConversationsData.data[_conversationCounter];
        conversationUIController.ChangeLabel(content);
    }
    private void EnablePlayerConversation()
    {
        conversationAnimUI.Enable();
        _enemyObjectAnimUI.Disable();
    }
    private void DisablePlayerConversation()
    {
        conversationAnimUI.Disable();
    }

    
    
    private void PrepareEnemyConversation()
    {
        SetEnemyConversationLabelText();
        EnableEnemyConversation();
    }
    private void SetEnemyConversationLabelText() 
    {
        var content = _enemyConversationsData[_conversationCounter].data[0];
        enemyObjectUIController.ChangeLabel(content);
    }
    private void EnableEnemyConversation()
    {
        conversationAnimUI.Disable();
        _enemyObjectAnimUI.Enable();
    }

    private void DisableEnemyConversation()
    {
        if(_enemyObjectAnimUI != null) _enemyObjectAnimUI.Disable();
    }
}
*/
