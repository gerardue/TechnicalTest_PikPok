using System;
using Game.Core.CharacterEntity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Systems.TurnBasedMechanicSystem.Controller
{
    public class SelectCharacterController : MonoBehaviour
    {
        [Header("Selectors")]
        [SerializeField]
        private Transform playerEntitySelector;
        [SerializeField]
        private Transform enemyEntitySelector;
        
        [Header("Layer Mask")]
        [SerializeField]
        private LayerMask layerMask;
        
        private Camera mainCamera;
        private Ray ray;
        private RaycastHit hit;

        private ICharacterEntity playerEntity; 
        private ICharacterEntity enemyEntity;

        public Action<ICharacterEntity> OnGetPlayerEntity;
        public Action<ICharacterEntity> OnGetEnemyEntity;
        
        #region Unity Methods

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && !EventSystem.current.IsPointerOverGameObject())
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        // playerEntitySelector.position =
                        //     Vector3.ProjectOnPlane(hit.collider.transform.position, Vector3.up);
                        //
                        // playerEntity = hit.collider.GetComponent<ICharacterEntity>(); 
                        // OnGetPlayerEntity?.Invoke(playerEntity);
                        SelectPlayer(hit.collider.gameObject);
                    }
                    else if (hit.collider.CompareTag("Enemy"))
                    {
                        // enemyEntitySelector.position =
                        //     Vector3.ProjectOnPlane(hit.collider.transform.position, Vector3.up);
                        //
                        // enemyEntity = hit.collider.GetComponent<ICharacterEntity>();
                        // OnGetEnemyEntity?.Invoke(enemyEntity);
                        SelectEnemy(hit.collider.gameObject);
                    }
                }
            }
        }
        
        #endregion

        #region Public Methods

        public void SelectPlayer(GameObject aPlayer)
        {
            playerEntitySelector.position =
                Vector3.ProjectOnPlane(aPlayer.transform.position, Vector3.up);
                        
            playerEntity = aPlayer.GetComponent<ICharacterEntity>(); 
            OnGetPlayerEntity?.Invoke(playerEntity);
        }
        
        public void SelectEnemy(GameObject aEnemy)
        {
            enemyEntitySelector.position =
                Vector3.ProjectOnPlane(aEnemy.transform.position, Vector3.up);
                        
            enemyEntity = aEnemy.GetComponent<ICharacterEntity>();
            OnGetEnemyEntity?.Invoke(enemyEntity);
        }

        #endregion
    }
}