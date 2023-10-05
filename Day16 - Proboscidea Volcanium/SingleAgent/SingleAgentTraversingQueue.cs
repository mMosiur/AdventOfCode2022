namespace AdventOfCode.Year2022.Day16.SingleAgent;

internal sealed partial class SingleAgentTraverser
{
	private sealed class TraversingQueue
	{
		private readonly Queue<TraversingState> _queue = new();

		public int Count => _queue.Count;

		public bool EnqueueIfValid(TraversingState state)
		{
			if (state.TimeLeft < 0) return false;
			Enqueue(state);
			return true;
		}

		public void Enqueue(TraversingState state)
		{
			_queue.Enqueue(state);
		}

		public TraversingState Dequeue()
		{
			return _queue.Dequeue();
		}

		public TraversingState Peek()
		{
			return _queue.Peek();
		}

		public bool TryDequeue(out TraversingState state)
		{
			return _queue.TryDequeue(out state);
		}
	}
}
