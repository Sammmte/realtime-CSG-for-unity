using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RealtimeCSG
{
	internal static class EntityIdRegistry
	{
		private static readonly Dictionary<int, EntityId> s_IntToEntityId = new Dictionary<int, EntityId>();
		private static readonly Dictionary<EntityId, int> s_EntityIdToInt = new Dictionary<EntityId, int>();
		private static int s_NextId = 1;

		public static int GetOrCreateId(EntityId entityId)
		{
			if (entityId == EntityId.None)
				return 0;
			if (s_EntityIdToInt.TryGetValue(entityId, out int id))
				return id;
			id = s_NextId++;
			s_IntToEntityId[id] = entityId;
			s_EntityIdToInt[entityId] = id;
			return id;
		}

		public static int GetOrCreateId(UnityEngine.Object obj)
		{
			if (obj == null || !obj)
				return 0;
			return GetOrCreateId(obj.GetEntityId());
		}

		public static EntityId GetEntityId(int id)
		{
			if (id == 0)
				return EntityId.None;
			if (s_IntToEntityId.TryGetValue(id, out var entityId))
				return entityId;
			return EntityId.None;
		}

#if UNITY_EDITOR
		public static UnityEngine.Object IdToObject(int id)
		{
			var entityId = GetEntityId(id);
			if (entityId == EntityId.None)
				return null;
			return EditorUtility.EntityIdToObject(entityId);
		}
#endif
	}
}
