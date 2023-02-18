using Godot;

public partial class ScoreLabel : Label {
    private int _score = 0;

	private float _lastSquash;

	public void OnMobSquashed()	{
		if (Time.GetTicksMsec() - _lastSquash > 10) {
			_lastSquash = Time.GetTicksMsec();
			_score += 1;
			Text = string.Format("Score: {0}", _score);
		}
		
	}
}
