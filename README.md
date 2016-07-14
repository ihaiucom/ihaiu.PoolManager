# ihaiu.PoolManager
email:ihaiu@qq.com
声明：在Unity PoolManager基础上改的。

#Class
<table>
	<tr>
		<td>PoolManager</td>
		<td>总入口类</td>
	</tr>


	<tr>
		<td>PoolGroupDict</td>
		<td>PoolManager的延伸，保存PoolGroup的字典。</td>
	</tr>


	<tr>
		<td>PoolGroup</td>
		<td>对象池组，保存Pool、Spawn实例对象、Despawn实例对象。</td>
	</tr>

	<tr>
		<td>ObjectPool</td>
		<td>对象池。</td>
	</tr>


	<tr>
		<td>ScriptableObjectPool</td>
		<td>继承自ObjectPool。如果是继承了ScriptableObject的类推荐使用这个。</td>
	</tr>

	<tr>
		<td>PrefabPool</td>
		<td>继承自ObjectPool<T>。预设的对象池。</td>
	</tr>

	<tr>
		<td>IPoolItem</td>
		<td>实例对象接口，自定义的类推荐实现它。</td>
	</tr>
</table>


#PoolManager.groups
  /** 创建PoolGroup */  public PoolGroup Create(string groupName)

 /** 销毁PoolGroup */  public bool Destroy(string groupName) 
 /** 销毁所有PoolGroup */  public void DestroyAll()


 /** 是否已经存在groupName的PoolGroup */ public bool ContainsKey(string groupName)

 /** 尝试获取groupName的PoolGroup */ public bool TryGetValue(string groupName, out PoolGroup poolGroup)

 /** 获取key的PoolGroup */ public PoolGroup this[string key]



 /** 默认PoolGroup，可以自己扩展 */ public PoolGroup common {             get             {                 if (!ContainsKey("CommonPoolGroup"))                 {                     Create("CommonPoolGroup");                 }                  return this["CommonPoolGroup"];             } }



