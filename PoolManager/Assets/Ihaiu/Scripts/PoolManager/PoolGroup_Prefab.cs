using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Ihaius
{
    public partial class PoolGroup 
    {

        /** 检测粒子设为闲置状态最长时间 */
        public float maxParticleDespawnTime = 300;

        #region Private Properties
        private List<PrefabPool> _prefabPools = new List<PrefabPool>();
        internal List<Transform> _spawned = new List<Transform>();
        private Dictionary<string, Transform> prefabs = new Dictionary<string, Transform>();
        #endregion Private Properties



        #region Constructor and Init

        private void OnDestruct_Prefab()
        {

            foreach (PrefabPool pool in this._prefabPools) pool.SelfDestruct();

            this._prefabPools.Clear();
            this._spawned.Clear();
        }


       
        public void CreatePool(PrefabPool prefabPool)
        {
            bool isAlreadyPool = this.GetPool(prefabPool.prefabGO) == null ? false : true;
            if (!isAlreadyPool)
            {
                prefabPool.poolGroup = this;
                this._prefabPools.Add(prefabPool);
                this.prefabs.Add(prefabPool.prefabGO.name, prefabPool.prefab);
            }

            prefabPool.inspectorInstanceConstructor();
            if (prefabPool.preloaded != true)
            {
                if (this.logMessages)
                    Debug.Log(string.Format("PoolGroup {0}: 预实例化对象中 preloadAmount={1} {2}",
                        this.groupName,
                        prefabPool.preloadAmount,
                        prefabPool.prefabGO.name));

                prefabPool.PreloadInstances();
            }
        }


     
        public void Add(Transform instance, string prefabName, bool despawn, bool parent)
        {
            for (int i = 0; i < this._prefabPools.Count; i++)
            {
                if (this._prefabPools[i].prefabGO == null)
                {
                    Debug.LogError("Unexpected Error: PrefabPool.prefab is null");
                    return;
                }

                if (this._prefabPools[i].prefabGO.name == prefabName)
                {
                    PrefabPool prefabPool = this._prefabPools[i];
                    prefabPool.AddUnpooled(instance, despawn);

                    if (this.logMessages)
                        Debug.Log(string.Format(
                            "PoolGroup {0}: Adding previously unpooled instance {1}",
                            this.groupName,
                            instance.name));

                    if (parent) instance.parent = prefabPool.parent;

                    // New instances are active and must be added to the internal list 
                    if (!despawn) this._spawned.Add(instance);

                    return;
                }
            }

            // Log an error if a PrefabPool with the given name was not found
            Debug.LogError(string.Format("PoolGroup {0}: PrefabPool {1} not found.",
                this.groupName,
                prefabName));

        }
        #endregion Constructor and Init






        #region Pool Functionality
        public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot, Transform parent)
        {
            Transform inst;

            #region Use from Pool
            for (int i = 0; i < this._prefabPools.Count; i++)
            {
                // Determine if the prefab was ever used as explained in the docs
                //   I believe a comparison of two references is processor-cheap.
                if (this._prefabPools[i].prefabGO == prefab.gameObject)
                {
                    PrefabPool prefabPool = this._prefabPools[i];
                    inst = prefabPool.SpawnInstance(pos, rot);

                    // This only happens if the limit option was used for this
                    //   Prefab Pool.
                    if (inst == null) return null;

                    if (parent != null)  // User explicitly provided a parent
                    {
                        inst.parent = parent;
                    }
                    else if (inst.parent != prefabPool.parent)  // Auto organize?
                    {
                        // If a new instance was created, it won't be grouped
                        inst.parent = prefabPool.parent;
                    }

                  
                    this._spawned.Add(inst);

                    prefabPool.ItemOnSpawned(inst);

                    return inst;
                }
            }
            #endregion Use from Pool


            #region New PrefabPool
            // The prefab wasn't found in any PrefabPools above. Make a new one
            PrefabPool newPrefabPool = new PrefabPool();
            newPrefabPool.prefab = prefab;
            this.CreatePool(newPrefabPool);

            // Spawn the new instance (Note: prefab already set in PrefabPool)
            inst = newPrefabPool.SpawnInstance(pos, rot);

            if (parent != null)  // User explicitly provided a parent
            {
                inst.parent = parent;
            }
            else  // Auto organize
            {
                inst.parent = newPrefabPool.parent;  
            }


            // New instances are active and must be added to the internal list 
            this._spawned.Add(inst);
            #endregion New PrefabPool

            newPrefabPool.ItemOnSpawned(inst);

            // Done!
            return inst;
        }


        /// <summary>
        /// See primary Spawn method for documentation.
        /// </summary>
        public Transform Spawn(Transform prefab, Vector3 pos, Quaternion rot)
        {
            Transform inst = this.Spawn(prefab, pos, rot, null);

            // Can happen if limit was used
            if (inst == null) return null;

            return inst;
        }


        /// <summary>
        /// See primary Spawn method for documentation.
        /// 
        /// Overload to take only a prefab and instance using an 'empty' 
        /// position and rotation.
        /// </summary>
        public Transform Spawn(Transform prefab)
        {
            return this.Spawn(prefab, Vector3.zero, Quaternion.identity);
        }


        /// <summary>
        /// See primary Spawn method for documentation.
        /// 
        /// Convienince overload to take only a prefab  and parent the new 
        /// instance under the given parent
        /// </summary>
        public Transform Spawn(Transform prefab, Transform parent)
        {
            return this.Spawn(prefab, Vector3.zero, Quaternion.identity, parent);
        }


        /// <summary>
        /// See primary Spawn method for documentation.
        /// 
        /// Overload to take only a prefab name. The cached reference is pulled  
        /// from the SpawnPool.prefabs dictionary.
        /// </summary>
        public Transform Spawn(string prefabName)
        {
            Transform prefab = this.prefabs[prefabName];
            return this.Spawn(prefab);
        }


        /// <summary>
        /// See primary Spawn method for documentation.
        /// 
        /// Convienince overload to take only a prefab name and parent the new 
        /// instance under the given parent
        /// </summary>
        public Transform Spawn(string prefabName, Transform parent)
        {
            Transform prefab = this.prefabs[prefabName];
            return this.Spawn(prefab, parent);
        }


        /// <summary>
        /// See primary Spawn method for documentation.
        /// 
        /// Overload to take only a prefab name. The cached reference is pulled from 
        /// the SpawnPool.prefabs dictionary. An instance will be set to the passed 
        /// position and rotation.
        /// </summary>
        public Transform Spawn(string prefabName, Vector3 pos, Quaternion rot)
        {
            Transform prefab = this.prefabs[prefabName];
            return this.Spawn(prefab, pos, rot);
        }


        /// <summary>
        /// See primary Spawn method for documentation.
        /// 
        /// Convienince overload to take only a prefab name and parent the new 
        /// instance under the given parent. An instance will be set to the passed 
        /// position and rotation.
        /// </summary>
        public Transform Spawn(string prefabName, Vector3 pos, Quaternion rot, 
            Transform parent)
        {
            Transform prefab = this.prefabs[prefabName];
            return this.Spawn(prefab, pos, rot, parent);
        }


        public AudioSource Spawn(AudioSource prefab,
            Vector3 pos, Quaternion rot)
        {
            return this.Spawn(prefab, pos, rot, null);  // parent = null
        }


        public AudioSource Spawn(AudioSource prefab)
        {
            return this.Spawn
                (
                    prefab, 
                    Vector3.zero, Quaternion.identity,
                    null  // parent = null
                );
        }


        public AudioSource Spawn(AudioSource prefab, Transform parent)
        {
            return this.Spawn
                (
                    prefab, 
                    Vector3.zero, 
                    Quaternion.identity,
                    parent
                );
        }


        public AudioSource Spawn(AudioSource prefab,
            Vector3 pos, Quaternion rot,
            Transform parent)
        {
            // Instance using the standard method before doing particle stuff
            Transform inst = Spawn(prefab.transform, pos, rot, parent);

            // Can happen if limit was used
            if (inst == null) return null;

            // Get the emitter and start it
            var src = inst.GetComponent<AudioSource>();
            src.Play();

            this.StartCoroutine(this.ListForAudioStop(src));

            return src;
        }


        /// <summary>
        /// See docs for SpawnInstance(Transform prefab, Vector3 pos, Quaternion rot)
        /// for basic functionalty information.
        ///     
        /// Pass a ParticleSystem component of a prefab to instantiate, trigger 
        /// emit, then listen for when all particles have died to "auto-destruct", 
        /// but instead of destroying the game object it will be deactivated and 
        /// added to the pool to be reused.
        /// 
        /// IMPORTANT: 
        ///     * You must pass a ParticleSystem next time as well, or the emitter
        ///       will be treated as a regular prefab and simply activate, but emit
        ///       will not be triggered!
        ///     * The listner that waits for the death of all particles will 
        ///       time-out after a set number of seconds and log a warning. 
        ///       This is done to keep the developer aware of any unexpected 
        ///       usage cases. Change the public property "maxParticleDespawnTime"
        ///       to adjust this length of time.
        /// 
        /// Broadcasts "OnSpawned" to the instance. Use this instead of Awake()
        ///     
        /// This function has the same initial signature as Unity's Instantiate() 
        /// that takes position and rotation. The return Type is different though.
        /// </summary>
        public ParticleSystem Spawn(ParticleSystem prefab,
            Vector3 pos, Quaternion rot)
        {
            return Spawn(prefab, pos, rot, null);  // parent = null

        }

        /// <summary>
        /// See primary Spawn ParticleSystem method for documentation.
        /// 
        /// Convienince overload to take only a prefab name and parent the new 
        /// instance under the given parent. An instance will be set to the passed 
        /// position and rotation.
        /// </summary>
        public ParticleSystem Spawn(ParticleSystem prefab,
            Vector3 pos, Quaternion rot,
            Transform parent)
        {
            // Instance using the standard method before doing particle stuff
            Transform inst = this.Spawn(prefab.transform, pos, rot, parent);

            // Can happen if limit was used
            if (inst == null) return null;

            // Get the emitter and start it
            var emitter = inst.GetComponent<ParticleSystem>();
            //emitter.Play(true);  // Seems to auto-play on activation so this may not be needed

            this.StartCoroutine(this.ListenForEmitDespawn(emitter));

            return emitter;
        }


        /// <summary>
        /// See docs for SpawnInstance(ParticleSystems prefab, Vector3 pos, Quaternion rot)
        /// This is the same but for ParticleEmitters
        /// 
        /// IMPORTANT: 
        ///     * This function turns off Unity's ParticleAnimator autodestruct if
        ///       one is found.
        /// </summary>
        public ParticleEmitter Spawn(ParticleEmitter prefab,
            Vector3 pos, Quaternion rot)
        {
            // Instance using the standard method before doing particle stuff
            Transform inst = this.Spawn(prefab.transform, pos, rot);

            // Can happen if limit was used
            if (inst == null) return null;

            // Make sure autodestrouct is OFF as it will cause null references
            var animator = inst.GetComponent<ParticleAnimator>();
            if (animator != null) animator.autodestruct = false;

            // Get the emitter
            var emitter = inst.GetComponent<ParticleEmitter>();
            emitter.emit = true;

            this.StartCoroutine(this.ListenForEmitDespawn(emitter));

            return emitter;
        }


        /// <summary>
        /// This will not be supported for Shuriken particles. This will eventually 
        /// be depricated.
        /// </summary>
        /// <param name="prefab">
        /// The prefab to instance. Not used if an instance already exists in 
        /// the scene that is queued for reuse. Type = ParticleEmitter
        /// </param>
        /// <param name="pos">The position to set the instance to</param>
        /// <param name="rot">The rotation to set the instance to</param>
        /// <param name="colorPropertyName">Same as Material.SetColor()</param>
        /// <param name="color">a Color object. Same as Material.SetColor()</param>
        /// <returns>The instance's ParticleEmitter</returns>
        public ParticleEmitter Spawn(ParticleEmitter prefab,
            Vector3 pos, Quaternion rot,
            string colorPropertyName, Color color)
        {
            // Instance using the standard method before doing particle stuff
            Transform inst = this.Spawn(prefab.transform, pos, rot);

            // Can happen if limit was used
            if (inst == null) return null;

            // Make sure autodestrouct is OFF as it will cause null references
            var animator = inst.GetComponent<ParticleAnimator>();
            if (animator != null) animator.autodestruct = false;

            // Get the emitter
            var emitter = inst.GetComponent<ParticleEmitter>();

            // Set the color of the particles, then emit
            emitter.GetComponent<Renderer>().material.SetColor(colorPropertyName, color);
            emitter.emit = true;

            this.StartCoroutine(ListenForEmitDespawn(emitter));

            return emitter;
        }


        /// <summary>
        /// If the passed object is managed by the SpawnPool, it will be 
        /// deactivated and made available to be spawned again.
        ///     
        /// Despawned instances are removed from the primary list.
        /// </summary>
        /// <param name="item">The transform of the gameobject to process</param>
        public void Despawn(Transform instance)
        {
            // Find the item and despawn it
            bool despawned = false;
            for (int i = 0; i < this._prefabPools.Count; i++)
            {
                if (this._prefabPools[i]._spawned.Contains(instance))
                {
                    despawned = this._prefabPools[i].DespawnInstance(instance);
                    break;
                }  // Protection - Already despawned?
                else if (this._prefabPools[i]._despawned.Contains(instance))
                {
                    Debug.LogError(
                        string.Format("SpawnPool {0}: {1} has already been despawned. " +
                            "You cannot despawn something more than once!",
                            this.groupName,
                            instance.name));
                    return;
                }
            }

            // If still false, then the instance wasn't found anywhere in the pool
            if (!despawned)
            {
                Debug.LogError(string.Format("SpawnPool {0}: {1} not found in SpawnPool",
                    this.groupName,
                    instance.name));
                return;
            }

            // Remove from the internal list. Only active instances are kept. 
            //   This isn't needed for Pool functionality. It is just done 
            //   as a user-friendly feature which has been needed before.
            this._spawned.Remove(instance);
        }


        /// <summary>
        /// See docs for Despawn(Transform instance) for basic functionalty information.
        ///     
        /// Convienince overload to provide the option to re-parent for the instance 
        /// just before despawn.
        /// </summary>
        public void Despawn(Transform instance, Transform parent)
        {
            instance.parent = parent;
            this.Despawn(instance);
        }


        /// <description>
        /// See docs for Despawn(Transform instance). This expands that functionality.
        ///   If the passed object is managed by this SpawnPool, it will be 
        ///   deactivated and made available to be spawned again.
        /// </description>
        /// <param name="item">The transform of the instance to process</param>
        /// <param name="seconds">The time in seconds to wait before despawning</param>
        public void Despawn(Transform instance, float seconds)
        {
            this.StartCoroutine(this.DoDespawnAfterSeconds(instance, seconds, false, null));
        }


        /// <summary>
        /// See docs for Despawn(Transform instance) for basic functionalty information.
        ///     
        /// Convienince overload to provide the option to re-parent for the instance 
        /// just before despawn.
        /// </summary>
        public void Despawn(Transform instance, float seconds, Transform parent)
        {
            this.StartCoroutine(this.DoDespawnAfterSeconds(instance, seconds, true, parent));
        }


        /// <summary>
        /// Waits X seconds before despawning. See the docs for DespawnAfterSeconds()
        /// the argument useParent is used because a null parent is valid in Unity. It will 
        /// make the scene root the parent
        /// </summary>
        private IEnumerator DoDespawnAfterSeconds(Transform instance, float seconds, bool useParent, Transform parent)
        {
            GameObject go = instance.gameObject;
            while (seconds > 0)
            {
                yield return null;

                // If the instance was deactivated while waiting here, just quit
                if (!go.activeInHierarchy)
                    yield break;

                seconds -= Time.deltaTime;
            }

            if (useParent)
                this.Despawn(instance, parent);
            else
                this.Despawn(instance);
        }


        /// <description>
        /// Despawns all active instances in this SpawnPool
        /// </description>
        public void DespawnAll()
        {
            var spawned = new List<Transform>(this._spawned);
            for (int i = 0; i < spawned.Count; i++)
                this.Despawn(spawned[i]);
        }


        /// <description>
        /// Returns true if the passed transform is currently spawned.
        /// </description>
        /// <param name="item">The transform of the gameobject to test</param>
        public bool IsSpawned(Transform instance)
        {
            return this._spawned.Contains(instance);
        }

        #endregion Pool Functionality



        #region Utility Functions
        /// <summary>
        /// Returns the prefab pool for a given prefab.
        /// </summary>
        /// <param name="prefab">The Transform of an instance</param>
        /// <returns>PrefabPool</returns>
        public PrefabPool GetPool(Transform prefab)
        {
            for (int i = 0; i < this._prefabPools.Count; i++)
            {
                if (this._prefabPools[i].prefabGO == null)
                    Debug.LogError(string.Format("SpawnPool {0}: PrefabPool.prefabGO is null",
                        this.groupName));

                if (this._prefabPools[i].prefabGO == prefab.gameObject)
                    return this._prefabPools[i];
            }

            // Nothing found
            return null;
        }


        /// <summary>
        /// Returns the prefab pool for a given prefab.
        /// </summary>
        /// <param name="prefab">The GameObject of an instance</param>
        /// <returns>PrefabPool</returns>
        public PrefabPool GetPool(GameObject prefab)
        {
            for (int i = 0; i < this._prefabPools.Count; i++)
            {
                if (this._prefabPools[i].prefabGO == null)
                    Debug.LogError(string.Format("SpawnPool {0}: PrefabPool.prefabGO is null",
                        this.groupName));

                if (this._prefabPools[i].prefabGO == prefab)
                    return this._prefabPools[i];
            }

            // Nothing found
            return null;
        }


        /// <summary>
        /// Returns the prefab used to create the passed instance. 
        /// This is provided for convienince as Unity doesn't offer this feature.
        /// </summary>
        /// <param name="instance">The Transform of an instance</param>
        /// <returns>Transform</returns>
        public Transform GetPrefab(Transform instance)
        {
            for (int i = 0; i < this._prefabPools.Count; i++)
                if (this._prefabPools[i].Contains(instance))
                    return this._prefabPools[i].prefab;

            // Nothing found
            return null;
        }


        /// <summary>
        /// Returns the prefab used to create the passed instance. 
        /// This is provided for convienince as Unity doesn't offer this feature.
        /// </summary>
        /// <param name="instance">The GameObject of an instance</param>
        /// <returns>GameObject</returns>
        public GameObject GetPrefab(GameObject instance)
        {
            for (int i = 0; i < this._prefabPools.Count; i++)
                if (this._prefabPools[i].Contains(instance.transform))
                    return this._prefabPools[i].prefabGO;

            // Nothing found
            return null;
        }


        private IEnumerator ListForAudioStop(AudioSource src)
        {
            // Safer to wait a frame before testing if playing.
            yield return null;

            while (src.isPlaying)
                yield return null;

            this.Despawn(src.transform);
        }


        /// <summary>
        /// Used to determine when a particle emiter should be despawned
        /// </summary>
        /// <param name="emitter">ParticleEmitter to process</param>
        /// <returns></returns>
        private IEnumerator ListenForEmitDespawn(ParticleEmitter emitter)
        {
            // This will wait for the particles to emit. Without this, there will
            //   be no particles in the while test below. I don't know why the extra 
            //   frame is required but should never be noticable. No particles can
            //   fade out that fast and still be seen to change over time.
            yield return null;
            yield return new WaitForEndOfFrame();

            // Do nothing until all particles die or the safecount hits a max value
            float safetimer = 0;   // Just in case! See Spawn() for more info
            while (emitter.particleCount > 0)
            {
                safetimer += Time.deltaTime;
                if (safetimer > this.maxParticleDespawnTime)
                    Debug.LogWarning
                    (
                        string.Format
                        (
                            "SpawnPool {0}: " +
                            "Timed out while listening for all particles to die. " +
                            "Waited for {1}sec.",
                            this.groupName,
                            this.maxParticleDespawnTime
                        )
                    );

                yield return null;
            }

            // Turn off emit before despawning
            emitter.emit = false;
            this.Despawn(emitter.transform);
        }

        // ParticleSystem (Shuriken) Version...
        private IEnumerator ListenForEmitDespawn(ParticleSystem emitter)
        {
            // Wait for the delay time to complete
            // Waiting the extra frame seems to be more stable and means at least one 
            //  frame will always pass
            yield return new WaitForSeconds(emitter.startDelay + 0.25f);

            // Do nothing until all particles die or the safecount hits a max value
            float safetimer = 0;   // Just in case! See Spawn() for more info
            while (emitter.IsAlive(true))
            {
                if (!emitter.gameObject.activeInHierarchy)
                {
                    emitter.Clear(true);
                    yield break;  // Do nothing, already despawned. Quit.
                }

                safetimer += Time.deltaTime;
                if (safetimer > this.maxParticleDespawnTime)
                    Debug.LogWarning
                    (
                        string.Format
                        (
                            "SpawnPool {0}: " +
                            "Timed out while listening for all particles to die. " +
                            "Waited for {1}sec.",
                            this.groupName,
                            this.maxParticleDespawnTime
                        )
                    );

                yield return null;
            }

            // Turn off emit before despawning
            //emitter.Clear(true);
            this.Despawn(emitter.transform);
        }

        #endregion Utility Functions

    }
}
