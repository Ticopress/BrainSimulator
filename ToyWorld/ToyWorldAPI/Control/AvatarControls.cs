﻿using System.Collections.Generic;

namespace GoodAI.ToyWorld.Control
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAvatarControls
    {
        /// <summary>
        /// Value is clamped to (-1,1). Negative values mean move backwards, positive are for forward movement.
        /// </summary>
        AvatarAction<float> DesiredSpeed { get; }

        /// <summary>
        /// Value is clamped to (-1,1). Negative values mean rotate left, positive are for rotation to the right.
        /// </summary>
        AvatarAction<float> DesiredRotation { get; }

        /// <summary>
        /// To interact with object in front.
        /// </summary>
        AvatarAction<bool> Interact { get; }

        /// <summary>
        /// To use tool in hand / punch.
        /// </summary>
        AvatarAction<bool> Use { get; }

        /// <summary>
        /// Pick up or put down tool in hand.
        /// </summary>
        AvatarAction<bool> PickUp { get; }

        /// <summary>
        /// Rewrites actions from this list with actions from parameter with lower priority value.
        /// </summary>
        /// <param name="actions"></param>
        void Update(IAvatarControls actions);
    }

    /// <summary>
    /// 
    /// </summary>
    public struct AvatarControls : IAvatarControls
    {
        private AvatarAction<float> m_desiredSpeed;
        private AvatarAction<float> m_desiredRotation;
        private AvatarAction<bool> m_interact;
        private AvatarAction<bool> m_use;
        private AvatarAction<bool> m_pickUp;

        /// <summary>
        /// Value is clamped to (-1,1). Negative values mean move backwards, positive are for forward movement.
        /// </summary>
        public AvatarAction<float> DesiredSpeed { get { return m_desiredSpeed; } set { m_desiredSpeed += value; } }

        /// <summary>
        /// Value is clamped to (-1,1). Negative values mean rotate left, positive are for rotation to the right.
        /// </summary>
        public AvatarAction<float> DesiredRotation { get { return m_desiredRotation; } set { m_desiredRotation += value; } }

        /// <summary>
        /// To interact with object in front.
        /// </summary>
        public AvatarAction<bool> Interact { get { return m_interact; } set { m_interact += value; } }

        /// <summary>
        /// To use tool in hand / punch.
        /// </summary>
        public AvatarAction<bool> Use { get { return m_use; } set { m_use += value; } }

        /// <summary>
        /// Pick up or put down tool in hand.
        /// </summary>
        public AvatarAction<bool> PickUp { get { return m_pickUp; } set { m_pickUp += value; } }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="desiredSpeed"></param>
        /// <param name="desiredRotation"></param>
        /// <param name="interact"></param>
        /// <param name="use"></param>
        /// <param name="pickUp"></param>
        public AvatarControls(
            int priority,
            float desiredSpeed = 0f,
            float desiredRotation = 0f,
            bool interact = false,
            bool use = false,
            bool pickUp = false
            )
            : this()
        {
            m_desiredSpeed = new AvatarAction<float>(desiredSpeed, priority);
            m_desiredRotation = new AvatarAction<float>(desiredRotation, priority);
            m_interact = new AvatarAction<bool>(interact, priority);
            m_use = new AvatarAction<bool>(use, priority);
            m_pickUp = new AvatarAction<bool>(pickUp, priority);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        public AvatarControls(IAvatarControls other)
            : this()
        {
            Update(other);
        }

        /// <summary>
        /// Rewrites actions from this AvatarControls with actions from given AvatarControls with lower priority value.
        /// </summary>
        /// <param name="actions"></param>
        public void Update(IAvatarControls actions)
        {
            DesiredSpeed = actions.DesiredSpeed;
            DesiredRotation = actions.DesiredRotation;
            Interact = actions.Interact;
            Use = actions.Use;
            PickUp = actions.PickUp;
        }
    }
}
