# ihaiu.PoolManager
email:ihaiu@qq.com ,  zengfeng75@qq.com  <br>
web: <a href="http://www.ihaiu.com" >http://ihaiu.com</a>  <br>
声明：在Unity PoolManager基础上改的。 <br>
欢迎使用，如有改良想法求分享。

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

#PoolManager
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** PoolGroup字典 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">static</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">PoolGroupDict</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> groups = </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">new</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">PoolGroupDict</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">();<br/></span>
</p>
<p>
    <br/>
</p>


#PoolManager.groups
 
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 创建PoolGroup */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">PoolGroup</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> Create(</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">string</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> groupName)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 销毁PoolGroup */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> Destroy(</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">string</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> groupName)<br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 销毁所有PoolGroup */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> DestroyAll()</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 是否已经存在groupName的PoolGroup */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> ContainsKey(</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">string</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> groupName)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 尝试获取groupName的PoolGroup */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> TryGetValue(</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">string</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> groupName, </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">out</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">PoolGroup</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> poolGroup)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 获取key的PoolGroup */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">PoolGroup</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">this</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">[</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">string</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> key]</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#9a9a9a;"><br/></span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 默认PoolGroup，可以自己扩展 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#9a9a9a;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">PoolGroup</span><span style=" font-family:&#39;Menlo&#39;; color:#9a9a9a;"> common<br/>{<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">get<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#9a9a9a;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">if</span><span style=" font-family:&#39;Menlo&#39;; color:#9a9a9a;"> (!ContainsKey(</span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">&quot;CommonPoolGroup&quot;</span><span style=" font-family:&#39;Menlo&#39;; color:#9a9a9a;">))<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Create(</span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">&quot;CommonPoolGroup&quot;</span><span style=" font-family:&#39;Menlo&#39;; color:#9a9a9a;">);<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">return</span><span style=" font-family:&#39;Menlo&#39;; color:#9a9a9a;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">this</span><span style=" font-family:&#39;Menlo&#39;; color:#9a9a9a;">[</span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">&quot;CommonPoolGroup&quot;</span><span style=" font-family:&#39;Menlo&#39;; color:#9a9a9a;">];<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }<br/>}</span>
</p>
<p>
    <br/>
</p>



#为了方便调用，扩展一个常用PoolGroup
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">namespace</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> Ihaius<br/>{<br/>&nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">partial</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">class</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">PoolGroupDict<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; {<br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 战斗对象池组 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">PoolGroup</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> war<br/>&nbsp; &nbsp; &nbsp; &nbsp; {<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">get<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">if</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> (!ContainsKey(</span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">&quot;WarPoolGroup&quot;</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">))<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; {<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Create(</span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">&quot;WarPoolGroup&quot;</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">);<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">return</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">this</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">[</span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">&quot;WarPoolGroup&quot;</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">];<br/>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; }<br/>&nbsp; &nbsp; &nbsp; &nbsp; }<br/><br/>&nbsp; &nbsp; }<br/>}<br/></span>
</p>
<p>
    <br/>
</p>


#PoolGroup属性
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 组名称 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">string</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> groupName;<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 是否打印日志信息 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> logMessages = </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">true</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">;<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 检测粒子设为闲置状态最长时间 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">float</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> maxParticleDespawnTime = </span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">300</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">;<br/></span>
</p>
<p>
    <br/>
</p>





#PoolGroup方法
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#535860;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#535860;">/** 添加一个对象池 */</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#424242;"><br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> CreatePool&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt;(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">ObjectPool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt; objectPool)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#424242;">/** 获取对应类型的Pool */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">ObjectPool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt; GetPool&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt;()</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">AbstractObjectPool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> GetPool(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Type</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> type)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#424242;">/** 获取一个对应类型的实例对象 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> Spawn&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt;(</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">params</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">object</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">[] args)<br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/>&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#424242;">/** 将实例对象设置为闲置状态 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> Despawn&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt;(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> inst)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/>&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#424242;">/** 延迟seconds秒，将实例对象设置为闲置状态 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> Despawn&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt;(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> instance, </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">float</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> seconds)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">/** 添加一个实例到对应的Pool。despawn:true为闲置状态;false为真正使用状态 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> Add&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt;(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> instance, </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> despawn)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p>
    <br/>
</p>



#PoolGroup用Prefab的方法

<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span><span style=" font-family:&#39;Menlo&#39;; color:#535860;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#535860;">/** 添加一个对象池 */</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#424242;"><br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> CreatePool(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">PrefabPool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> prefabPool)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#424242;">/** 获取对应预设的Pool */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> PrefabPool GetPool(Transform prefab)<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> PrefabPool GetPool(GameObject prefab)<br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">/** 获取预设*/</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> GetPrefab(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> instance)<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">GameObject</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> GetPrefab(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">GameObject</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> instance)<br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 添加实例对象instance到名称为prefabName对象池</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// despawn,true为闲置状态;false为真正使用状态,并把该对象添加到SpawnPool的_spawned列表</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// parent, true时instance.parent=group</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Add(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> instance, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">string</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefabName, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> despawn, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> parent)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 检测声音停止播放协程</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 如果声音停止播放了就调用Despawn。设置该对象为闲置状态</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">private</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> IEnumerator ListForAudioStop(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">AudioSource</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> src)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 检测粒子生命结束协程</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 如果粒子gameObject没激活就取消检测。emitter.IsAlive(</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">true</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">)==false时就调用Despawn。设置该对象为闲置状态</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 如果检测时间超过maxParticleDespawnTime就抛出一个警告</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">private</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> IEnumerator ListenForEmitDespawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">ParticleEmitter</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> emitter)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">private</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> IEnumerator ListenForEmitDespawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">ParticleSystem</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> emitter)</span>
</p>
<p>
    <br/>
</p>


<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 从对象池获取一个实例对象或者创建一个实例对象</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 检测是否存在prefab的对象池</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 如果存在，就从prefabPool里拿一个实例对象。如果这个实例对象是null就返回null；否则就设置pos, rot。parent为空就设置为group。最后在该gameObejct广播OnSpawned消息<br/>// 如果不存在,就创建一个PrefabPool。然后和上面一样的步骤</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefab, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Vector3</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> pos, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Quaternion</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> rot, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> parent)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefab, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Vector3</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> pos, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Quaternion</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> rot)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefab)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefab, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> parent)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">string</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefabName)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">string</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefabName, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> parent)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">string</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefabName, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Vector3</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> pos, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Quaternion</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> rot)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">string</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefabName, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Vector3</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> pos, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Quaternion</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> rot, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> parent)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 从对象池获取一个声音实例对象或者创建一个声音实例对象</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 如果实例对象是null，就返回null</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 获取该对象的声音组件AudioSource, 并调用 play()<br/>// 启动检测声音停止播放协程，如果声音停止播放了就调用Despawn。设置该对象为闲置状态</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">AudioSource</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">AudioSource</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefab, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Vector3 pos, Quaternion rot)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">AudioSource</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">AudioSource</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefab)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">AudioSource</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">AudioSource</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefab, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> parent)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">AudioSource</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">AudioSource</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefab, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Vector3 pos, Quaternion rot, Transform parent)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 从对象池获取一个粒子特效实例对象或者创建一个粒子实例对象</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 如果实例对象是null，就返回null</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 启动检测粒子生命结束协程，如果粒子gameObject没激活就取消检测。emitter.IsAlive(</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">true</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">)==false时就调用Despawn。设置该对象为闲置状态</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 如果检测时间超过maxParticleDespawnTime就抛出一个警告</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">ParticleSystem</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">ParticleSystem</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefab, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Vector3 pos, Quaternion rot)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">ParticleSystem</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">ParticleSystem</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefab, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Vector3 pos, Quaternion rot, Transform parent)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">ParticleEmitter</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">ParticleEmitter</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefab, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Vector3 pos, Quaternion rot)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">ParticleEmitter</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Spawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">ParticleEmitter</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> prefab, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Vector3 pos, Quaternion rot, string colorPropertyName, Color color)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 将一个实例对象设置为闲置状态</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Despawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> instance)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Despawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> instance, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> parent)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 延迟将一个实例对象设置为闲置状态</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Despawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> instance, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">float</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> seconds)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> Despawn(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> instance, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">float</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> seconds, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Transform</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> parent)</span>
</p>
<p>
    <br/>
</p>


#ObjectPool<T>属性

<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">/ 缓存池这个Prefab的预加载数量。意思为一开始加载的数量！</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">int</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> preloadAmount = </span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">1</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">;</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 如果勾选表示缓存池所有的gameobject可以“异步”加载。</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> preloadTime = </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">false</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">;</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 每几帧加载一个。</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">int</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> preloadFrames = </span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">2</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">;</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 延迟多久开始加载。</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">float</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> preloadDelay = </span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">0</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">;</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 是否开启对象实例化的限制功能。</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> limitInstances = </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">false</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">;</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 限制实例化Prefab的数量，也就是限制缓冲池的数量，它和上面的preloadAmount是有冲突的，如果同时开启则以limitAmout为准。</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">int</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> limitAmount = </span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">100</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">;</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 如果我们限制了缓存池里面只能有10个Prefab，如果不勾选它，那么你拿第11个的时候就会返回null。如果勾选它在取第11个的时候他会返回给你前10个里最不常用的那个。</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> limitFIFO = </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">false</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">;</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 是否开启缓存池智能自动清理模式。</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> cullDespawned = </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">false</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">;</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 缓存池自动清理，但是始终保留几个对象不清理。</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">int</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> cullAbove = </span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">50</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">;</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 每过多久执行一遍自动清理，单位是秒。从上一次清理过后开始计时</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">int</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> cullDelay = </span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">60</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">;</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 每次自动清理几个游戏对象。</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">int</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> cullMaxPerPass = </span><span style=" font-family:&#39;Menlo&#39;; color:#f99000;">5</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">;</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 是否打印日志信息</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> logMessages = false</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#565656;">// 强制关闭打印日志</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">private</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;"> forceLoggingSilent = </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">false</span><span style=" font-family:&#39;Menlo&#39;; color:#565656;">;</span>
</p>
<p>
    <br/>
</p>

#ObjectPool<T>方法
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 构造方法<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 设置 prefabGO = prefab.gameObject;<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 新建 _spawned = new List&lt;T&gt;();<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 新建 _despawned = new List&lt;T&gt;();<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">internal</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> inspectorInstanceConstructor()<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 析构方法<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 当SpawnPool.OnDestroy时调用<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 销毁_spawned,_despawned两个列表里的对象, 并清空两个列表<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 设置 prefab,prefabGO,spawnPool为null<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">internal</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> SelfDestruct()<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 缓存自动清理是否启动了，用来检测是否启动清理<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">private</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> cullingActive = </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">false</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">;<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 正在被使用的对象列表<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">internal</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> List&lt;T&gt; _spawned = </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">new</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> List&lt;T&gt;();<br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">List</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#d1636a;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt; spawned { </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">get</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> { </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">return</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">new</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">List</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#d1636a;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt;(</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">this</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">.</span><span style=" font-family:&#39;Menlo&#39;; color:#d1636a;">_spawned</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">); } }<br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 没有被使用的闲置对象列表<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">internal</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">List</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#d1636a;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt; _despawned = </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">new</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">List</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#d1636a;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt;();<br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">List</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#d1636a;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt; despawned { </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">get</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> { </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">return</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">new</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">List</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&lt;</span><span style=" font-family:&#39;Menlo&#39;; color:#d1636a;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&gt;(</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">this</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">.</span><span style=" font-family:&#39;Menlo&#39;; color:#d1636a;">_despawned</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">); } }<br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 对象的总数 = 使用的数量 + 闲置的数量<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">int</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> totalCount<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 已经预加载<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 在PreloadInstances（）方法中用到，用来判断是否需要执行预加载<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">private</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> _preloaded = </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">false</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">;<br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">internal</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> preloaded<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 设置对象为闲置<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 将对象xfrom从 _spawned转到_despawned<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// sendEventMessage=true时会对xfrom对象广播OnDespawned消息<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 设置xfrom.gameObejct.SetActive(false)<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 检测是否需要启动自动清理<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">internal</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> DespawnInstance(</span><span style=" font-family:&#39;Menlo&#39;; color:#d1636a;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> xform)<br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">internal</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> DespawnInstance(T xform, </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> sendEventMessage)<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 清理闲置对象协程<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">internal</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> IEnumerator CullDespawned()</span>
</p>
<p>
    <br/>
</p>


<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/><br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 获取一个实例对象<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 如果限制了实例对象数量，且正在使用的对象数量大于限制的数量，且开启了limitFIFO，那么就把_spawned[0]设置为闲置状态<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 如果闲置列表里有对象，就从限制列表里取出第一个对象；并把这个对象放到_spawned列表<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 如果限制列表没有对象，就新建一个对象<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">internal</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> T SpawnInstance(Vector3 pos, Quaternion rot)<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 创建一个实例对象<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 如果限制了实例对象数量，且对象总数大于限制的数量。就返回一个空对象 return null<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 否则 实例化一个对象，并检查SpawnPool的设置，对该对象进行设置。最后返回该创建的对象<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> T SpawnNew() { </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">return</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">this</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">.</span><span style=" font-family:&#39;Menlo&#39;; color:#d1636a;">SpawnNew</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Vector3</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">.zero, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Quaternion</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">.identity); }<br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#d1636a;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> SpawnNew(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Vector3</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> pos, </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">Quaternion</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> rot)<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 递归设置xform和他childs的layer<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">private</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> SetRecursively(T xform, </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">int</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> layer)<br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 将以个势力对象添加到缓存池。 despawn=true时就加到_despwan闲置列表，并设置gameObject.active=false；否则就加到_spawn正在被使用列表<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">internal</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> AddUnpooled(T inst, </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> despawn)<br/><br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 预实例化对象<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 如果preloaded=true。就表示已经预实例过了，终止执行<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 如果开启实例对象数量限制limitInstances=true且预实例数量大于限制数量preloadAmount &gt; limitAmount。那边就设置预实例数量等于限制数量preloadAmount = limitAmount;<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 如果开启异步预实例对象，就开启延迟异步预实例化对象协程。如果每帧预实例化数量大于预实例化数量preloadFrames &gt; preloadAmount，那么就设置每帧预实例化数量等于预实例化数量preloadFrames preloadAmount<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">internal</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> PreloadInstances()<br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 延迟异步预实例化对象<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">private</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> IEnumerator PreloadOverTime()<br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 检查是否包含了该对象。spawned、despawned从这两个列表里检查<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">public</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> Contains(T T)<br/>&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">// 再实例对象的名字后面加上（totalCount + 1 ）<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp; &nbsp; &nbsp; &nbsp; </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">private</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> nameInstance(T instance)<br/><br/></span>
</p>
<p>
    <br/>
</p>


#ObjectPool<T>扩展方法
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 实例化一个对象 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">virtual</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">protected</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> Instantiate(</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">params</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">object</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">[] arg)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#9a9a9a;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 调用对象方法--实例对象重设参数 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">virtual</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">protected</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> ItemSetArg(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> instance, </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">params</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">object</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">[] args)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#9a9a9a;">/** 给实例对象重命名 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">virtual</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">protected</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> nameInstance(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> instance)<br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">/** 调用对象方法--销毁 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">virtual</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">protected</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> ItemDestruct(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> instance)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">/** 调用对象方法--设置为闲置状态消息 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">virtual</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">protected</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> ItemOnDespawned(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> instance)</span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;">&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">/** 调用对象方法--设置为使用状态消息 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">virtual</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">internal</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> ItemOnSpawned(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> instance)<br/><br/><br/>&nbsp;</span><span style=" font-family:&#39;Menlo&#39;; font-style:italic; color:#4179b3;">/** 调用对象方法--设置是否激活 */<br/></span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">virtual</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">protected</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">void</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> ItemSetActive(</span><span style=" font-family:&#39;Menlo&#39;; color:#4179b3;">T</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> instance, </span><span style=" font-family:&#39;Menlo&#39;; color:#00a5a6;">bool</span><span style=" font-family:&#39;Menlo&#39;; color:#424242;"> value)<br/></span>
</p>
<p style="margin-top: 0px; margin-bottom: 0px;">
    <span style=" font-family:&#39;Menlo&#39;; color:#424242;"><br/></span>
</p>
<p>
    <br/>
</p>




