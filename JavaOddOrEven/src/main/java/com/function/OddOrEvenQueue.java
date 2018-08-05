package com.function;

import com.microsoft.azure.functions.annotation.*;

import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.methods.*;
import org.apache.http.entity.*;
import org.apache.http.impl.client.*;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.logging.Logger;

import com.microsoft.azure.functions.*;

/**
 * Azure Functions with Azure Storage Queue trigger.
 */
public class OddOrEvenQueue {

    public static CloseableHttpClient client = HttpClients.createDefault();
    /**
     * This function will be invoked when a new message is received at the specified path. The message contents are provided as input to this function.
     * @throws IOException
     * @throws ClientProtocolException
     */
    @FunctionName("OddOrEvenQueue")
    public void queueHandler(
        @QueueTrigger(name = "myNumber", queueName = "numbers", connection = "AzureWebJobsStorage") String myNumber,
        final ExecutionContext context
    ) throws NumberFormatException, ClientProtocolException, IOException {
        try
        {
            context.getLogger().info("Java Odd or Even Queue trigger function processed a message.");
            int number = Integer.parseInt(myNumber);

            if(number % 2 == 0) 
            {
                context.getLogger().info("Was even");
                HttpPost httpPost = new HttpPost("http://importantapi.com/api/transaction");
                httpPost.setEntity(new StringEntity("Even"));
                CloseableHttpResponse response = client.execute(httpPost);
                response.close();
            }
            else 
            {
                context.getLogger().info("Was odd");
                HttpPost httpPost = new HttpPost("http://importantapi.com/api/transaction");
                httpPost.setEntity(new StringEntity("Odd"));
                CloseableHttpResponse response = client.execute(httpPost);
            }
        } 
        catch(Exception e) 
        {
            throw e;
        }
        finally 
        {
        }
    }
}
