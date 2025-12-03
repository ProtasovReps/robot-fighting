using System.Collections.Generic;
using Extensions;
using UnityEngine;

namespace InputSystem
{
    public class DirectionValidationFactory : MonoBehaviour
    {
        [SerializeField] private DistanceValidator _leftDistanceValidator;
        [SerializeField] private DistanceValidator _rightDistanceValidator;

        public Dictionary<float, DistanceValidator> Produce()
        {
            return new Dictionary<float, DistanceValidator>
            {
                { Directions.Left, _leftDistanceValidator },
                { Directions.Right, _rightDistanceValidator }
            };
        }
    }
}