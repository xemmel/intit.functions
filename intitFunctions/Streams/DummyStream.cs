namespace intitFunctions;

public class DummyStream : Stream
{
    private readonly Stream _stream;

    public DummyStream(Stream stream)
    {
        _stream = stream;
    }
    public override bool CanRead => _stream.CanRead;

    public override bool CanSeek => _stream.CanSeek;

    public override bool CanWrite => _stream.CanWrite;

    public override long Length => _stream.Length;

    public override long Position
    {
        get => _stream.Position;
        set => _stream.Position = value;
    }

    public override void Flush()
    {
        _stream.Flush();
    }

    private void CopyBuffer(byte[] sourceBuffer, byte[] targetBuffer, int count)
    {
        for (int i = 0; i < count; i++)
        {
            targetBuffer[i] = sourceBuffer[i];
        }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        //return _stream.Read(buffer: DoIt(buffer), offset: offset,count:count);
        //buffer = DoIt(buffer);
        byte[] newBuffer = new byte[buffer.Length];
        int bytesReturned = _stream
                                .Read(
                                    buffer: newBuffer,
                                    offset: offset,
                                    count: count);
                                
        CopyBuffer(sourceBuffer: DoIt(newBuffer), targetBuffer: buffer, count: bytesReturned);
        return bytesReturned;
    }

    private byte[] DoIt(byte[] buffer)
    {
        var result = new byte[buffer.Length];
        for (int i = 0; i < buffer.Length; i++)
        {
            var b = buffer[i];
            if (b == 77)
                b++;
            result[i] = b;
        }
        return result;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return _stream.Seek(offset: offset, origin: origin);
    }

    public override void SetLength(long value)
    {
        _stream.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotImplementedException();
    }
}