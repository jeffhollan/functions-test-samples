package com.function;

import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.CloseableHttpClient;
import org.junit.Test;
import static org.mockito.Mockito.*;

import com.microsoft.azure.functions.*;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.logging.Logger;

import static org.junit.Assert.assertEquals;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.doReturn;
import static org.mockito.Mockito.mock;


/**
 * Unit test for OddOrEven class.
 */
public class OddOrEvenQueueTest {
    /**
     * Unit test for even numbers.
     */
    @Test
    public void testEvenNumbers() throws Exception {
        CloseableHttpClient client = mock(CloseableHttpClient.class);
        CloseableHttpResponse res = mock(CloseableHttpResponse.class);
        doReturn(res).when(client).execute(any());

        OddOrEvenQueue.client = client;
        
        OddOrEvenQueue function = new OddOrEvenQueue();
        function.queueHandler("2", generateContext());

        verify(client).execute(argThat(h -> {
            try 
            {
                HttpPost req = (HttpPost)h;
                BufferedReader reader = new BufferedReader(new InputStreamReader(req.getEntity().getContent()));
                String content = reader.readLine();
                assertEquals("Even", content);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }));
    }

    @Test
    public void testOddNumbers() throws Exception {
        CloseableHttpClient client = mock(CloseableHttpClient.class);
        CloseableHttpResponse res = mock(CloseableHttpResponse.class);
        doReturn(res).when(client).execute(any());

        OddOrEvenQueue.client = client;
        
        OddOrEvenQueue function = new OddOrEvenQueue();
        function.queueHandler("3", generateContext());

        verify(client).execute(argThat(h -> {
            try 
            {
                HttpPost req = (HttpPost)h;
                BufferedReader reader = new BufferedReader(new InputStreamReader(req.getEntity().getContent()));
                String content = reader.readLine();
                assertEquals("Odd", content);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }));
    }

    @Test(expected = NumberFormatException.class)
    public void testNonNumbers() throws Exception {
        OddOrEvenQueue function = new OddOrEvenQueue();
        function.queueHandler("I'm Even", generateContext());
    }

    private ExecutionContext generateContext()
    {
        final ExecutionContext context = mock(ExecutionContext.class);
        doReturn(Logger.getGlobal()).when(context).getLogger();

        return context;
    }
}
