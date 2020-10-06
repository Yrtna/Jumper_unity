using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class SimulatedSceneLogic : MonoBehaviour
    {
        public GameObject prefabToSim;
        public GameObject floor;
        public int stepsPerLoop = 10;

        private PhysicsScene _physicsScene;
        private GameObject _objectToSim;


        private PlayerController _playerController;
        public List<Vector3> simulatedTrajectory = new List<Vector3>();

        private void Start()
        {
            var newScene =
                SceneManager.CreateScene("PhysicsSim", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
            _physicsScene = newScene.GetPhysicsScene();

            var oldActiveScene = SceneManager.GetActiveScene();
            SceneManager.SetActiveScene(newScene);

            _objectToSim = Instantiate(prefabToSim);
            Destroy(_objectToSim.GetComponent<SimpleTrajectoryPreview>());
            var simFloor = Instantiate(floor);
            _objectToSim.AddComponent<SimulatedObjectLogic>();

            _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            SceneManager.SetActiveScene(oldActiveScene);
        }

        private void FixedUpdate()
        {
            simulatedTrajectory = GetSimulatedTrajectory();
        }

        private List<Vector3> SimulateTrajectory(Vector3 force)
        {
            _objectToSim.transform.position = Vector3.zero;
            var simulatedObj = _objectToSim.GetComponent<SimulatedObjectLogic>();
            Debug.Log($"SimulatedForce: {force.x}, {force.y}");
            simulatedObj.Launch(force);

            var trajectory = new List<Vector3>();
            for (var i = 0; i < stepsPerLoop; i++)
            {
                trajectory.Add(simulatedObj.GetPosition());
                _physicsScene.Simulate(Time.fixedDeltaTime);
            }

            return trajectory;
        }

        public List<Vector3> GetSimulatedTrajectory()
        {
            return SimulateTrajectory(_playerController.GenerateForce());
        }
    }
}