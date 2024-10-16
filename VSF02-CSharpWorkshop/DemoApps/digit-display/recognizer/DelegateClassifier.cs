namespace digits;

public class DelegateClassifier : Classifier
{
    public DistanceCalculation Distance { get; set; }

    public DelegateClassifier(string name, Record[] trainingData,
        DistanceCalculation distance) : base(name, trainingData)
    {
        Name = name;
        TrainingData = trainingData;
        Distance = distance;
    }

    public override Task<Prediction> Predict(Record input)
    {
        return Task.Run(() =>
        {
            int[] inputImage = input.Image;
            int best_total = int.MaxValue;
            Record best = new(0, Array.Empty<int>());
            foreach (Record candidate in TrainingData)
            {
                int total = 0;
                int[] candidateImage = candidate.Image;
                for (int i = 0; i < 784; i++)
                {
                    int diff = inputImage[i] - candidateImage[i];
                    total += Distance(diff);
                }
                if (total < best_total)
                {
                    best_total = total;
                    best = candidate;
                }
            }

            return new Prediction(input, best);
        });
    }
}
