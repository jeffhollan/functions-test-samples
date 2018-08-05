package com.function;

import org.junit.Test;
import org.mockito.internal.matchers.InstanceOf;

import static org.mockito.Mockito.*;

import com.microsoft.azure.functions.*;

import java.util.HashMap;
import java.util.Map;
import java.util.Optional;
import java.util.logging.Logger;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertSame;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.ArgumentMatchers.anyString;
import static org.mockito.Mockito.doReturn;
import static org.mockito.Mockito.mock;


/**
 * Unit test for OddOrEven class.
 */
public class OddOrEvenTest {
    /**
     * Unit test for even numbers.
     */
    @Test
    public void testEvenNumbers() throws Exception {
        // Setup

        final Map<String, String> queryParams = new HashMap<>();
        queryParams.put("number", "2");

        final HttpResponseMessage.Builder builder = mock(HttpResponseMessage.Builder.class);

        HttpRequestMessage<Optional<String>> req = generateHttpRequest(queryParams, builder);
        ExecutionContext context = generateContext();
        
        // Invoke
        new OddOrEven().HttpTriggerJava(req, context);

        verify(builder).body("Even");
        verify(req).createResponseBuilder(HttpStatus.OK);
    }

    @Test
    public void testOddNumbers() throws Exception {
        // Setup

        final Map<String, String> queryParams = new HashMap<>();
        queryParams.put("number", "3");

        final HttpResponseMessage.Builder builder = mock(HttpResponseMessage.Builder.class);

        HttpRequestMessage<Optional<String>> req = generateHttpRequest(queryParams, builder);
        ExecutionContext context = generateContext();
        
        // Invoke
        new OddOrEven().HttpTriggerJava(req, context);

        verify(builder).body("Odd");
        verify(req).createResponseBuilder(HttpStatus.OK);
    }

    @Test
    public void testNonNumber() throws Exception {
        // Setup

        final Map<String, String> queryParams = new HashMap<>();
        queryParams.put("number", "I'm Even");

        final HttpResponseMessage.Builder builder = mock(HttpResponseMessage.Builder.class);

        HttpRequestMessage<Optional<String>> req = generateHttpRequest(queryParams, builder);
        ExecutionContext context = generateContext();
        
        // Invoke
        new OddOrEven().HttpTriggerJava(req, context);

        verify(builder).body("Unable to parse");
        verify(req).createResponseBuilder(HttpStatus.BAD_REQUEST);
    }

    private HttpRequestMessage<Optional<String>> generateHttpRequest(Map<String, String> queryParams, HttpResponseMessage.Builder builder) {
        
        final HttpRequestMessage<Optional<String>> req = mock(HttpRequestMessage.class);
        doReturn(queryParams).when(req).getQueryParameters();

        final Optional<String> queryBody = Optional.empty();
        doReturn(queryBody).when(req).getBody();

        doReturn(builder).when(req).createResponseBuilder(any(HttpStatus.class));
        doReturn(builder).when(builder).body(anyString());

        final HttpResponseMessage res = mock(HttpResponseMessage.class);
        doReturn(res).when(builder).build();

        return req;
    }

    private ExecutionContext generateContext()
    {
        final ExecutionContext context = mock(ExecutionContext.class);
        doReturn(Logger.getGlobal()).when(context).getLogger();

        return context;
    }
}
