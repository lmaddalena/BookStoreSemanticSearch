using Microsoft.Extensions.AI;
using Pgvector;

namespace BookStore.Api.Services;

public class EmbeddingService(IEmbeddingGenerator<string, Embedding<float>> generator) : IEmbeddingService
{
    public Task<Vector> GenerateBookEmbeddingAsync(string title, string description, string author, CancellationToken cancellationToken = default)
    {
        var text = $"{title}. {description} Author: {author}";
        return GenerateVectorAsync(text, cancellationToken);
    }

    public Task<Vector> GenerateQueryEmbeddingAsync(string query, CancellationToken cancellationToken = default)
        => GenerateVectorAsync(query, cancellationToken);

    private async Task<Vector> GenerateVectorAsync(string text, CancellationToken cancellationToken)
    {
        var embedding = await generator.GenerateVectorAsync(text, cancellationToken: cancellationToken);
        return new Vector(embedding.ToArray());
    }
}